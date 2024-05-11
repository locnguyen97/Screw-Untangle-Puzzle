using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace _IECModules
{
    public class GoogleSheetToCSV : MonoBehaviour
    {
        //CSV reader from https://bravenewmethod.com/2014/09/13/lightweight-csv-reader-for-unity/

        static readonly string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        static readonly string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
        static readonly char[] TRIM_CHARS = {'\"'};

        private Action<List<List<string>>> m_OnComplete;

        private Action<string> m_OnError;

        private const string StartDocIdSearch = "spreadsheets/d/";
        private const string EndDocIdSearch = "/";
        private const string SpreadSheetId = "gid";

        [NaughtyAttributes.Button]
        public void TestDownloadFromUrl()
        {
            var url = "https://docs.google.com/spreadsheets/d/1siFrtchtGuq8gQYQMX19_z7ZpdBpoQQRDZnHiNsKrUA/edit#gid=927018210"; 
            // url = "https://docs.google.com/spreadsheets/d/1siFrtchtGuq8gQYQMX19_z7ZpdBpoQQRDZnHiNsKrUA/edit"; 
            print(GetDocId(url));
            print(GetSheetId(url));
            DownLoadCsv(url, false, data =>
            {
                data.ForEach(x => x.ForEach(print));
            }, print);
        }

        public static string GetBetweenString(string original, string start, string end)
        {
            var pFrom = original.IndexOf(start, StringComparison.Ordinal) + start.Length;
            var pTo = original.LastIndexOf(end, StringComparison.Ordinal);

            return original.Substring(pFrom, pTo - pFrom);
        }

        private static string GetDocId(string url)
        {
            return GetBetweenString(url, StartDocIdSearch, EndDocIdSearch);
        }

        private static string GetSheetId(string url)
        {
            url = url.Replace("#", "?");
            var uri = new Uri(url);
            var decodeParam = DecodeQueryParameters(uri); 
            return decodeParam.ContainsKey(SpreadSheetId) ? decodeParam[SpreadSheetId] : null; 
        }
        
        public static Dictionary<string, string> DecodeQueryParameters(Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");

            if (uri.Query.Length == 0)
                return new Dictionary<string, string>();

            return uri.Query.TrimStart('?')
                .Split(new[] { '&', ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(parameter => parameter.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries))
                .GroupBy(parts => parts[0],
                    parts => parts.Length > 2 ? string.Join("=", parts, 1, parts.Length - 1) : (parts.Length > 1 ? parts[1] : ""))
                .ToDictionary(grouping => grouping.Key,
                    grouping => string.Join(",", grouping));
        }

        public static string ParseGoogleSheetUrl(string url)
        {
            return ParseGoogleSheetUrl(GetDocId(url), GetSheetId(url));
        }

        public static string ParseGoogleSheetUrl(string docId, string sheetId)
        {
            var url = $"https://docs.google.com/spreadsheets/d/{docId}/export?format=csv";
            if (!string.IsNullOrEmpty(sheetId))
                url += "&gid=" + sheetId;
            return url;
        }

        public void DownLoadCsv(string url, bool skipFirstLine = false, Action<List<List<string>>> onComplete = null,
            Action<string> onError = null)
        {
            DownLoadCsv(GetDocId(url), GetSheetId(url), skipFirstLine, onComplete, onError);
        }

        public void DownLoadCsv(string docId, string sheetId, bool skipFirstLine,
            Action<List<List<string>>> onComplete = null,
            Action<string> onError = null)
        {
            // StartCoroutine(DownloadCSVCoroutine(docId, sheetId, skipFirstLine));
            StartCoroutine(DownloadCsvCoroutine(ParseGoogleSheetUrl(docId, sheetId), skipFirstLine, onComplete, onError));
        }

        private IEnumerator DownloadCsvCoroutine(string url, bool skipFirstLine,
            Action<List<List<string>>> onComplete, Action<string> onError)
        {
            Debug.Log("DownloadCSV: " + url);
            var www = UnityEditorHelper.GetWebRequestCsvFromAuthUrl(url);

            yield return www.SendWebRequest();

            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.LogError("Error downloading: " + www.error);
                onError?.Invoke("Error downloading: " + www.error);
                yield break;
            }

            List<List<string>> parseCSV = null;
            try
            {
                var csvContent = www.downloadHandler.text;
                Debug.Log("csvContent: ");
                Debug.Log(csvContent);
                parseCSV = JsonConvert.DeserializeObject<List<List<string>>>(csvContent);
                if (skipFirstLine) parseCSV.RemoveAt(0);
            }
            catch (Exception e)
            {
                Debug.LogError($"{e.Message}\n{e.StackTrace}");
                onError?.Invoke($"{e.Message}\n{e.StackTrace}");
                yield break;
            }

            if (parseCSV == null)
            {
                Debug.LogError("Unknown Error, null Data");
                onError?.Invoke("Unknown Error, null Data");
                yield break;
            }

            onComplete?.Invoke(parseCSV);
        }

        public static List<List<string>> ParseCSV(string text, bool skipFirstLine)
        {
            text = CleanReturnInCsvTexts(text);

            var list = new List<List<string>>();
            var lines = Regex.Split(text, LINE_SPLIT_RE);

            var header = Regex.Split(lines[0], SPLIT_RE);

            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (skipFirstLine && i == 0) continue;

                var values = Regex.Split(line, SPLIT_RE);

                var entry = new List<string>();
                for (var j = 0; j < header.Length && j < values.Length; j++)
                {
                    var value = values[j];
                    value = DecodeSpecialCharsFromCSV(value);
                    entry.Add(value);
                }

                list.Add(entry);
            }

            return list;
        }

        private static string CleanReturnInCsvTexts(string text)
        {
            text = text.Replace("\"\"", "'");

            if (text.IndexOf("\"") > -1)
            {
                string clean = "";
                bool insideQuote = false;
                for (int j = 0; j < text.Length; j++)
                {
                    if (!insideQuote && text[j] == '\"')
                    {
                        insideQuote = true;
                    }
                    else if (insideQuote && text[j] == '\"')
                    {
                        insideQuote = false;
                    }
                    else if (insideQuote)
                    {
                        if (text[j] == '\n')
                            clean += "<br>";
                        else if (text[j] == ',')
                            clean += "<c>";
                        else
                            clean += text[j];
                    }
                    else
                    {
                        clean += text[j];
                    }
                }

                text = clean;
            }

            return text;
        }

        private static string DecodeSpecialCharsFromCSV(string value)
        {
            value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "").Replace("<br>", "\n")
                .Replace("<c>", ",");
            return value;
        }
    }
}
