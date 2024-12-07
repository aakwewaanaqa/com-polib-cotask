using UnityEngine;
using UnityEngine.Networking;

namespace Polib.CoTasks.Exts.Web
{
    public static class WebExts
    {
        public static UnityWebRequestAsyncOperation Get(
            this string url,
            DownloadHandler handler = null)
        {
            var req                                      = new UnityWebRequest(url);
            if (handler is not null) req.downloadHandler = handler;
            return req.SendWebRequest();
        }

        public static UnityWebRequestAsyncOperation Post(
            this string               url,
            DownloadHandler           handler = null,
            params (string, string)[] fields)
        {
            WWWForm form = new();
            foreach (var field in fields) form.AddField(field.Item1, field.Item2);
            var    req                                   = UnityWebRequest.Post(url, form);
            if (handler is not null) req.downloadHandler = handler;
            return req.SendWebRequest();
        }
    }
}