using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Core.Response;

namespace Gepard.Core.FileHandling
{
    public class FileDescription
    {
        private FileInfo FileInfo { get; set; }

        public FileDescription(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
        }

        public byte[] GetAllBytes()
        {
            return File.ReadAllBytes(FileInfo.FullName);
        }

        public ulong GetFileSize()
        {
            return (ulong)FileInfo.Length;
        }

        public byte[] GetRangeBytes(HttpRange httpRange)
        {
            var bytesArray = new List<byte>();
            var fileContent = File.ReadAllBytes(FileInfo.FullName);

            for (var i = httpRange.Min; i <= httpRange.Max; i++)
            {
                bytesArray.Add(fileContent[i]);
            }

            return bytesArray.ToArray();
        }

        public string GetExtension()
        {
            return FileInfo.Extension;
        }

        public string GetEncoding()
        {
            var bom = new byte[4];
            using (var file = new FileStream(FileInfo.FullName, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return "utf-7";
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return "utf-8";
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return "utf-32";
            return "windows-1251";
        }

        public DateTime GetLastModified()
        {
            return FileInfo.LastWriteTime;
        }
    }
}
