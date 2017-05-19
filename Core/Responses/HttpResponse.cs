using System;
using System.Text;
using Gepard.Core.HttpFields;

namespace Gepard.Core.Responses
{
    public class HttpResponse
    {
        public HttpHeaders Headers { get; set; }
        public HttpContent Content { get; set; }
        public HttpVersion HttpVersion { get; set; }

        public int HttpStatusCode { get; set; }
        public bool IsSuccessStatus { get; set; }

        public HttpResponse()
        {
            Headers = new HttpHeaders();
            Content = new HttpContent();
            HttpVersion = new HttpVersion();
        }

        public byte[] GetBytes()
        {
            if (!Content.IncludeBody)
            {
                Content.Data = null;
            }

            Headers.Add("Accept-ranges", "bytes");
            Headers.Add("Content-Length", Content.Data?.Length.ToString() ?? "0");
            
            var data = $"{HttpVersion} {HttpStatusCode}\r\n";
            
            foreach (var title in Headers.HeadersList)
            {
                data += $"{title.Key}:{title.Value}\r\n";
            }
            
            data += "\r\n";
            
            return Content.Data != null ? CombineBytes(Encoding.UTF8.GetBytes(data), Content.Data) : Encoding.UTF8.GetBytes(data);
        }

        private static byte[] CombineBytes(byte[] first, byte[] second)
        {
            var ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }
    }
}
