using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Configuration.Server;

namespace Gepard.Controllers
{
    public class GetController : IController
    {
        public ServerConfig ServerConfig { get; set; }

        public Response Execute(Request request)
        {
            
            var file = Path.Combine(ServerConfig.DirectoryRoot, "www", request.VirtualHost.Directory, request.Uri.Url);
            if (File.Exists(file))
            {
                return MakeFromFile(new FileInfo(file));
            }
            else if (Directory.Exists(file))
            {
                var directoryInfo = new DirectoryInfo(file);
                var fileList = directoryInfo.GetFiles();
                foreach (var f in fileList)
                {
                    var fileName = f.Name;
                    if (fileName.Contains("index.html") || fileName.Contains("index.htm"))
                    {
                        return MakeFromFile(f);
                    }
                }
                return MakeErrorFromFile(404);
            }
            else
            {
                return MakeErrorFromFile(404);
            }
        }
    }
}
