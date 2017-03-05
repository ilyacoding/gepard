using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Data
{
    public class Response
    {
        private string HttpVersion { get; set; }
        private int AnswerCode { get; set; }
        private string Date { get; set; }
        private string Server { get; set; }
        private string LastModified { get; set; }
        private string ContentLanguage { get; set; }
        private string ContentType { get; set; }
        private string ContentLength { get; set; }
        private string Charset { get; set; }
        private string Connection { get; set; }

        private string Content { get; set; }

        public Response()
        {

        }

        public override string ToString()
        {
            return "";
        }
    }
}
