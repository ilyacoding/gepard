using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Core.HttpFields
{
    public class HttpContentType
    {
        public string Value { get; set; }
        public string Boundary { get; set; }

        public HttpContentType(string str)
        {
            try
            {
                var fields = str.Split(';').ToList();
                Value = fields[0];

                if (fields.Count >= 2)
                {
                    Boundary = fields[1].Split('=')[1] ?? "";
                }
            }
            catch
            {
                Value = "";
                Boundary = null;
            }
        }
    }
}
