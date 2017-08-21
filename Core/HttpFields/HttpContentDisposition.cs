using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Core.HttpFields
{
    public class HttpContentDisposition
    {
        public string Main { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }

        public HttpContentDisposition(string str)
        {
            var parts = str.Split(';').ToList();
            Main = parts[0];
            parts.RemoveAt(0);

            var dictionary = parts.Select(part => part.Split(new[] {'='}, 2)).ToDictionary(keyValue => keyValue[0].Trim().ToLower(), keyValue => keyValue[1].Trim(new char[] {'"', ' '}));
            if (dictionary.ContainsKey("name"))
            {
                Name = dictionary["name"];
            }

            if (dictionary.ContainsKey("filename"))
            {
                FileName = dictionary["filename"];
            }
        }
    }
}
