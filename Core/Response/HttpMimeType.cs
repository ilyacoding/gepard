using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Core.Response
{
    public class HttpMimeType
    {   
        private static readonly Dictionary<string, List<string>> ExtensionDictionary = new Dictionary<string, List<string>>
        {
            { "text", new List<string> { "html", "htm", "xht", "xhtml", "css", "js", "txt" } },
            { "image", new List<string> { "jpg", "jpeg", "tif", "tiff", "png", "gif", "bmp", "dib" } }
        };

        public static string GetByExtension(string extension)
        {
            extension = extension.Trim('.', ' ');
            foreach (var element in ExtensionDictionary)
            {
                if (element.Value.Contains(extension))
                {
                    return element.Key;
                }
            }
            return ExtensionDictionary.First().Key;
        }
    }
}
