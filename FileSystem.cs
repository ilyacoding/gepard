using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard
{
    public class FileSystem
    {
        public string Path { get; set; }

        public FileSystem(string path)
        {
            Path = path;
        }

        public bool IsDirectory()
        {
            return Directory.Exists(Path);
        }

        public bool IsFile()
        {
            return File.Exists(Path);
        }
    }
}
