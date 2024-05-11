using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadFile : MonoBehaviour
{
    IEnumerator DoDownloadFile(string url, string savePath, System.Action<bool, string> OnDownloadDone)
    {
        var uwr = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET);
        Debug.Log("url:" + url);
        Debug.Log("url 2:" + uwr.url);
        // string savePath = Path.Combine(Application.persistentDataPath, "unity3d.html");
        uwr.downloadHandler = new DownloadHandlerFile(savePath);
        yield return uwr.SendWebRequest();
        
        Debug.Log("Download done");
        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(uwr.error);
            Debug.LogError(uwr.url);
            OnDownloadDone?.Invoke(false, uwr.error);
        }
        else
        {
            Debug.Log("File successfully downloaded and saved to " + savePath);
            OnDownloadDone?.Invoke(true, savePath);
        }
    }

    public void Download(string url, string savePath, Action<bool, string> onDownloadDone)
    {
        StartCoroutine(DoDownloadFile(url, savePath, onDownloadDone));
    }
}
