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
        private string DirectoryRoot { get; set; }
        private string ServerName { get; set; }

        public FileHandler(string directoryRoot, string serverName)
        {
            DirectoryRoot = directoryRoot;
            ServerName = serverName;
        }

        public byte[] MakeFromFile(FileInfo fileInfo)
        {
            var filePath = fileInfo.FullName;
            return File.ReadAllBytes(filePath);
            //return new Response(ResponseStatus.Get(200), data, MimeType.GetByExtension(fileInfo.Extension));
        }

        public byte[] MakeErrorFromFile(int errorCode)
        {
            var data = File.ReadAllText(Path.Combine(DirectoryRoot, "pages", "error.html"));
            data = data.Replace("{CODE}", errorCode.ToString());
            data = data.Replace("{CODE-DESCRIPTION}", ResponseStatus.Get(errorCode));
            data = data.Replace("{SERVER}", ServerName + " / " + Environment.OSVersion);
            return Encoding.UTF8.GetBytes(data);
        }
    }
}
