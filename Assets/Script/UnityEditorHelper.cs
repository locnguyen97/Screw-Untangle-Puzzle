using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public static class UnityEditorHelper
{
    public static void SetDirtyAndSave(ScriptableObject scriptObj)
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(scriptObj);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }

    public static UnityWebRequest GetWebRequestCsvFromAuthUrl(string url)
    {
        var hostUrl = "https://us-central1-united-data.cloudfunctions.net/google-sheets-middleman-2";
        var dictContent = new Dictionary<string, string>
        {
            ["token"] = "WinterWolf2020",
            ["sheetUrl"] = url,
            ["format"] = "jsonarray"
        };

        var uwr = new UnityWebRequest(hostUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dictContent));
        uwr.uploadHandler = new UploadHandlerRaw(bodyRaw);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        return uwr;
    }

}