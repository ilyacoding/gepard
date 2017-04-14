using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Configuration.Server;
using Gepard.Core;

namespace Gepard.Models
{
    public class GetModel : IModel
    {
        public ServerConfig ServerConfig { get; set; }
        public FileHandler FileHandler { get; set; }

        public GetModel(ServerConfig serverConfig, FileHandler fileHandler)
        {
            ServerConfig = serverConfig;
            FileHandler = fileHandler;
        }

        public Response Execute(Request request)
        {
            var file = Path.Combine(ServerConfig.DirectoryRoot, "www", request.VirtualHost.Directory, request.Uri.Url);
            if (File.Exists(file))
            {
                return FileHandler.MakeFromFile(new FileInfo(file));
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
                        return FileHandler.MakeFromFile(f);
                    }
                }
                return FileHandler.MakeErrorFromFile(404);
            }
            else
            {
                return FileHandler.MakeErrorFromFile(404);
            }
        }
    }
}
