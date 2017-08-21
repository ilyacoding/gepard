using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Core.HttpFields.Body
{
    public class HttpBodyObject
    {
        public HttpContentDisposition ContentDisposition { get; set; }
        public HttpContentType ContentType { get; set; }
        public string Content { get; set; }

        public HttpBodyObject(IReadOnlyDictionary<string, string> fields, string content)
        {
            if (fields != null)
            {
                if (fields.ContainsKey("content-disposition"))
                {
                    ContentDisposition = new HttpContentDisposition(fields["content-disposition"]);
                }
                else
                {
                    throw new Exception();
                }

                if (fields.ContainsKey("content-type"))
                {
                    ContentType = new HttpContentType(fields["content-type"]);
                }
            }
            
            Content = content;
        }
    }
}
