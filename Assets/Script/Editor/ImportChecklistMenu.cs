using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using _IECModules;
using Facebook.Unity.Settings;
using Google;
using UnityEngine;
using UnityEditor;

public class ImportChecklistMenu : EditorWindow
{
    private string m_ChecklistUrl;
    private string URL_SAVE = "ImportChecklistMenu.URL_SAVE";

    private GoogleSheetToCSV m_GoogleToCSV;
    private DownloadFile m_DownloadFile;

    [MenuItem("IEC/Import Checklist")]
    static void ShowImportCheckList()
    {
        EditorWindow.GetWindow(typeof(ImportChecklistMenu));
    }

    private void OnGUI()
    {
        if (string.IsNullOrEmpty(m_ChecklistUrl))
        {
            m_ChecklistUrl = PlayerPrefs.GetString(URL_SAVE,
                "https://docs.google.com/spreadsheets/d/1K22AuAIobys30hme-1Nw6o9KdsjVMKGjUP0sBd_xsq4/edit#gid=927018210");
        }

        if (m_GoogleToCSV == null)
        {
            m_GoogleToCSV = FindObjectOfType<GoogleSheetToCSV>();
        }

        if (m_DownloadFile == null)
        {
            m_DownloadFile = FindObjectOfType<DownloadFile>();
        }

        m_ChecklistUrl = EditorGUILayout.TextField("Checklist Url", m_ChecklistUrl);
        if (GUILayout.Button("Import Id from checklist"))
        {
            LogEditorHelper.ClearLog();
            PlayerPrefs.SetString(URL_SAVE, m_ChecklistUrl);
            PlayerPrefs.Save();
            SetIdFromChecklist();
        }

        LogEditorHelper.EditorOnGUI();
    }

    private void SetIdFromChecklist()
    {
        var configId = AssetDatabase.LoadAssetAtPath<IdConfig>("Assets/Script/Config/ServicesConfig.asset");
        var keystoreName = string.Empty;
        configId.projectChecklistUrl = m_ChecklistUrl;
        m_GoogleToCSV.DownLoadCsv(m_ChecklistUrl, true, data =>
        {
            var rowCount = data.Count;
            for (int i = 0; i < rowCount; i++)
            {
                var row = data[i];
                var fieldCount = row.Count;
                var field = row[0];
                var value = string.Empty;
                if (fieldCount > 1)
                {
                    value = row[1];
                    value = value.Trim();
                }

                switch (field)
                {
                    case "Display Name":
                        LogEditorHelper.AddLog($"Product Name: {value}");
                        FacebookSettings.AppLabels = new List<string> {value};
                        PlayerSettings.productName = value;
                        break;
                    case "Facebook ID":
                        var facebookId = value;
                        LogEditorHelper.AddLog($"Facebook ID: {facebookId}");
                        if (Facebook.Unity.Settings.FacebookSettings.NullableInstance == null)
                        {
                            var instance = EditorWindow.CreateInstance<Facebook.Unity.Settings.FacebookSettings>();
                            var path1 = Path.Combine(Application.dataPath, "FacebookSDK/SDK/Resources");
                            if (!Directory.Exists(path1))
                                Directory.CreateDirectory(path1);
                            var path2 =
                                Path.Combine(Path.Combine("Assets", "FacebookSDK/SDK/Resources"),
                                    "FacebookSettings.asset");
                            AssetDatabase.CreateAsset(instance, path2);
                        }

                        Selection.activeObject = Facebook.Unity.Settings.FacebookSettings.Instance;
                        Facebook.Unity.Settings.FacebookSettings.AppIds = new List<string> {facebookId};
                        Facebook.Unity.Settings.FacebookSettings.AppLabels =
                            new List<string> {PlayerSettings.productName};
                        EditorUtility.SetDirty(Facebook.Unity.Settings.FacebookSettings.Instance);
                        Facebook.Unity.Editor.ManifestMod.GenerateManifest();
                        break;
                    case "Adjust Android ID":
                        LogEditorHelper.AddLog($"Adjust ID Android: {value}");
                        configId.adjustId = value;
                        break;
                    case "Support Email":
                        LogEditorHelper.AddLog($"Support Email: {value}");
                        configId.emailSupport = value;
                        break;
                    case "Keystore Link":
                        DownloadKeystore(value, keystoreName);
                        break;
                    case "Keystore Name":
                        LogEditorHelper.AddLog($"Keystore Name:{value}");
                        keystoreName = value;
                        break;
                    case "BundleID":
                        var bundleId = value;
                        LogEditorHelper.AddLog($"Bundle ID: {bundleId}");
                        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, bundleId);
                        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, bundleId);
                        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, bundleId);
                        break;
                }
            }

            EditorUtility.SetDirty(configId);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        });
    }

    void DownloadKeystore(string keystoreLink, string keystoreName)
    {
        if (string.IsNullOrEmpty(keystoreLink))
        {
            LogEditorHelper.AddLogError("Missing keystore link");
            return;
        }
        
        if (string.IsNullOrEmpty(keystoreName))
        {
            LogEditorHelper.AddLogError("Missing keystore name");
            return;
        }
        LogEditorHelper.AddLog($"Download Keystore:{keystoreLink}");
        m_DownloadFile.Download(keystoreLink, $"{Directory.GetCurrentDirectory()}\\{keystoreName}.keystore", (r, s) =>
        {
            LogEditorHelper.AddLog(r ? "Download keystore done" : "Download keystore error");
        });
    }
}