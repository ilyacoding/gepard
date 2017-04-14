using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Configuration.Server;

namespace Gepard
{
    public class FileHandler
    {
        private ServerConfig ServerConfig { get; set; }

        public FileHandler(ServerConfig serverConfig)
        {
            ServerConfig = serverConfig;
        }

        public Response MakeFromFile(FileInfo fileInfo)
        {
            var filePath = fileInfo.FullName;
            var data = File.ReadAllBytes(filePath);
            return new Response(ResponseStatus.Get(200), data, MimeType.GetByExtension(fileInfo.Extension));
        }

        public Response MakeErrorFromFile(int errorCode)
        {
            var data = File.ReadAllText(Path.Combine(ServerConfig.DirectoryRoot, "pages", "error.html"));
            data = data.Replace("{CODE}", errorCode.ToString());
            data = data.Replace("{CODE-DESCRIPTION}", ResponseStatus.Get(errorCode));
            data = data.Replace("{SERVER}", ServerConfig.ServerName + " / " + Environment.OSVersion);
            return new Response(ResponseStatus.Get(errorCode), Encoding.UTF8.GetBytes(data), "text");
        }
    }
}
