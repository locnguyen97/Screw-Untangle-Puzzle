using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public static class LogEditorHelper
{
    private const string LOG_SET_ID_SDK = "log_set_id_sdk";

    public static List<LogData> listSetSdkLog;

    private static Color? m_DefaultTextColor;

    public static void EditorOnGUI()
    {
        if (m_DefaultTextColor == null) m_DefaultTextColor = GUI.skin.textArea.normal.textColor;
        if (listSetSdkLog == null) listSetSdkLog = new List<LogData>();
        if (listSetSdkLog.Count == 0)
        {
            var savedLogData = PlayerPrefs.GetString(LOG_SET_ID_SDK);
            if (!string.IsNullOrEmpty(savedLogData))
                listSetSdkLog = JsonConvert.DeserializeObject<List<LogData>>(savedLogData);
        }

        listSetSdkLog.ForEach(logData =>
        {
            var guiStyle = GUI.skin.textArea;
            guiStyle.normal.textColor = guiStyle.focused.textColor = m_DefaultTextColor.Value;
            if (logData.logType == LogType.Warning)
                guiStyle.normal.textColor = guiStyle.focused.textColor = new Color(1f, 0.5f, 0f);
            if (logData.logType == LogType.Error) guiStyle.normal.textColor = guiStyle.focused.textColor = Color.red;
            EditorGUILayout.TextArea(logData.log, guiStyle);
        });
    }

    public static void AddLogError(string log)
    {
        AddLog($"Error: {log}", LogType.Error);
    }

    public static void AddLogWarning(string log)
    {
        AddLog($"Warning: {log}", LogType.Warning);
    }

    public static void AddLog(object log)
    {
        AddLog(log.ToString());
    }

    public static void AddLog(string log, LogType logType = LogType.Debug)
    {
        var logStr = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {log}";
        if (listSetSdkLog == null)
        {
            ClearLog();
        }

        if (listSetSdkLog.Count > 0 && listSetSdkLog.Last().logType == LogType.Debug && logType == LogType.Debug)
        {
            listSetSdkLog.Last().log += $"\n{logStr}";
        }
        else
        {
            listSetSdkLog.Add(new LogData
            {
                log = logStr,
                logType = logType
            });
        }

        SaveLogToPlayerPref();
    }

    public static void ClearLog()
    {
        listSetSdkLog = new List<LogData>();
        SaveLogToPlayerPref();
    }

    public static void SaveLogToPlayerPref()
    {
        PlayerPrefs.SetString(LOG_SET_ID_SDK, JsonConvert.SerializeObject(listSetSdkLog));
    }
}

public class LogData
{
    public string log;
    public LogType logType = LogType.Debug;
}

public enum LogType
{
    Debug,
    Warning,
    Error
}