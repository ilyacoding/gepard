using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Configuration.VirtualHost;

namespace Gepard.Core.FileHandling
{
    public class FileSystem
    {
        private List<IndexFile> IndexFiles { get; set; }
        public string Path { get; set; }
        
        public FileSystem(string path, List<IndexFile> indexFiles)
        {
            Path = path;
            IndexFiles = indexFiles;
        }

        private bool IsDirectory()
        {
            return Directory.Exists(Path);
        }

        private bool IsFile()
        {
            return File.Exists(Path);
        }

        public FileDescription GetFile()
        {
            if (IsFile())
            {
                return new FileDescription(new FileInfo(Path));
            }
            else if (IsDirectory())
            {
                var directoryInfo = new DirectoryInfo(Path);
                foreach (var fileInfo in directoryInfo.GetFiles())
                {
                    var fileName = fileInfo.Name;
                    foreach (var indexFile in IndexFiles)
                    {
                        if (fileName.Contains(indexFile.FileName))
                        {
                            return new FileDescription(fileInfo);
                        }
                    }
                }
            }

            return null;
        }
    }
}
