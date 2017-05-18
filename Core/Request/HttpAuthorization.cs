using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Core.Request
{
    public class HttpAuthorization
    {
        public string AuthType { get; set; }
        private Dictionary<string, string> Fields { get; set; }
        public string this[string key] => Fields.ContainsKey(key.ToLower()) ? Fields[key.ToLower()] : "";

        public HttpAuthorization(string str)
        {
            Fields = new Dictionary<string, string>();

            try
            {
                var parts = str.Split(new[] { ' ' }, 2);
                AuthType = parts[0];
                var fields = parts[1].Split(new[] { ',' }).ToList();
                if (fields.Count == 1)
                {
                    Fields.Add("value", fields[0]);
                    return;
                }

                foreach (var field in fields)
                {
                    var keyValue = field.Trim().Split(new[] { '=' }, 2);
                    Fields.Add(keyValue[0].Trim().ToLower(), keyValue[1].Trim(new[] { '"' }));
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}
