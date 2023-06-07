using System;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using ComponentAce.Compression.Libs.zlib;
using System.Threading.Tasks;
using Tarkov.Base.Core;
using System.Net.Http;

namespace Tarkov.Base.Utilities
{
    public class Req : IDisposable
    {
        private static HttpClient client;
        public static string Session;
        public static string RemoteEndPoint;
        public bool isUnity;

        private static Req _instance;
        public static Req Instance
        {
            get
            {
                _instance ??= new Req();
                return _instance;
            }
        }

        public Req()
        {
            if (string.IsNullOrEmpty(Session))
                Session = PatchConstants.GetPHPSESSID();
            if (string.IsNullOrEmpty(RemoteEndPoint))
                RemoteEndPoint = PatchConstants.GetBackendUrl();
            client = new();
            client.Timeout = new TimeSpan(0, 0, 0, 0, 1000);
            var builder = new UriBuilder(RemoteEndPoint);
            builder.Port = int.Parse(RemoteEndPoint.Split(':')[2]);
            client.BaseAddress = builder.Uri;
        }

        public Req(string session, string remoteEndPoint)
        {
            Session = session;
            RemoteEndPoint = remoteEndPoint;
            client = new();
            client.Timeout = new TimeSpan(0, 0, 0, 0, 1000);
            var builder = new UriBuilder(RemoteEndPoint);
            builder.Port = int.Parse(RemoteEndPoint.Split(':')[2]);
            client.BaseAddress = builder.Uri;
        }

        /// <summary>
        /// Send request to the server and get Stream of data back
        /// </summary>
        /// <param name="url">String url endpoint example: /start</param>
        /// <param name="method">POST or GET</param>
        /// <param name="data">string json data</param>
        /// <param name="compress">Should use compression gzip?</param>
        /// <returns>Stream or null</returns>
        private Stream Send(string url, string method = "GET", string data = null, bool compress = true)
        {

            // disable SSL encryption
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            var fullUri = url;
            if (!Uri.IsWellFormedUriString(fullUri, UriKind.Absolute))
                fullUri = RemoteEndPoint + fullUri;

            if (!string.IsNullOrEmpty(Session))
            {
                client.DefaultRequestHeaders.Add("Cookie", $"PHPSESSID={Session}");
                client.DefaultRequestHeaders.Add("SessionId", Session);
            }

            client.DefaultRequestHeaders.Add("Accept-Encoding", "deflate");

            if (method != "GET" && !string.IsNullOrEmpty(data))
            {
                // set request body
                byte[] bytes = compress ? SimpleZlib.CompressToBytes(data, zlibConst.Z_BEST_COMPRESSION) : Encoding.UTF8.GetBytes(data);

                client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                if (compress)
                {
                    client.DefaultRequestHeaders.Add("content-encoding", "deflate");
                }
                var byteArrayContent = new ByteArrayContent(bytes, 0, bytes.Length);
                try
                {
                    var res = client.PostAsync(url, byteArrayContent).Result;

                    return res.Content.ReadAsStreamAsync().Result;
                }
                catch (Exception e)
                {
                    if (isUnity)
                        Debug.LogError(e);
                }
                return null;
            }

            // get response stream
            try
            {
                return client.GetStreamAsync(url).Result;
            }
            catch (Exception e)
            {
                if (isUnity)
                    Debug.LogError(e);
            }

            return null;
        }

        public byte[] GetData(string url)
        {
            var ms = new MemoryStream();
            var dataStream = Send(url, "GET");
            if (dataStream != null)
            {
                dataStream.CopyTo(ms);

                return ms.ToArray();
            }
            return null;
        }

        public void PutJson(string url, string data, bool compress = true)
        {
            using Stream stream = Send(url, "PUT", data, compress);
        }

        public string GetJson(string url, bool compress = true)
        {
            using Stream stream = Send(url, "GET", null, compress);
            using MemoryStream ms = new();
            if (stream == null)
                return "";
            stream.CopyTo(ms);
            return SimpleZlib.Decompress(ms.ToArray(), null);
        }

        public string PostJson(string url, string data, bool compress = true)
        {
            using Stream stream = Send(url, "POST", data, compress);
            using MemoryStream ms = new();
            if (stream == null)
                return "";
            stream.CopyTo(ms);
            return SimpleZlib.Decompress(ms.ToArray(), null);
        }

        public async Task<string> PostJsonAsync(string url, string data, bool compress = true)
        {
            return await Task.Run(() => { return PostJson(url, data, compress); });
        }

        public Texture2D GetImage(string url, bool compress = true)
        {
            using Stream stream = Send(url, "GET", null, compress);
            using MemoryStream ms = new();
            if (stream == null)
                return null;
            Texture2D texture = new(8, 8);

            stream.CopyTo(ms);
            texture.LoadImage(ms.ToArray());
            return texture;
        }

        public void Dispose()
        {
        }
    }
}
