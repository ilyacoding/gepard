using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Core.HttpFields.Body
{
    public class HttpBody
    {
        public List<HttpBodyObject> HttpBodyObjects { get; set; }

        public HttpBody(string boundary, string content)
        {
            HttpBodyObjects = new List<HttpBodyObject>();

            if (boundary == null)
            {
                HttpBodyObjects.Add(new HttpBodyObject(null, content));
            }

            var body = content.Split(new string[] {"--" + boundary}, StringSplitOptions.RemoveEmptyEntries).ToList();
            body.Remove(body.Last());

            foreach (var el in body)
            {
                var parts = el.Split(new[] { "\r\n\r\n" }, 2, StringSplitOptions.None).ToList();
                parts = parts.Select(x => x.Trim()).ToList();

                var headers = HttpHeaders.Parse(parts[0]);
                var value = parts[1];
                HttpBodyObjects.Add(new HttpBodyObject(headers, value));
            }
        }
    }
}
