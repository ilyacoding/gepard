using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Gepard.Configuration.Server;
using Gepard.Core;
using Gepard.Core.HttpAction;
using Gepard.Core.Response;

namespace Gepard.Controllers
{
    public class GetMethodHandler : IRequestHandler
    {
        private string DirectoryRoot { get; set; }
        private FileHandler FileHandler { get; set; }
        public IRequestHandler NextHandler { get; set; }

        public GetMethodHandler(string directoryRoot, FileHandler fileHandler)
        {
            DirectoryRoot = directoryRoot;
            FileHandler = fileHandler;
        }

        public IHttpAction Handle(Request request)
        {
            if (request.Method == "GET")
            {
                //if (request.VirtualHost.AuthConfigs.Any(x => x.AuthDirectory == ))
                
                var fileSystem = new FileSystem(Path.Combine(DirectoryRoot, "www", request.VirtualHost.Directory, request.Uri.Url));

                if (fileSystem.IsFile())
                {
                    var fileInfo = new FileInfo(fileSystem.Path);
                    return new Ok(FileHandler.MakeFromFile(fileInfo), MimeType.GetByExtension(fileInfo.Extension));
                }
                else if (fileSystem.IsDirectory())
                {
                    var directoryInfo = new DirectoryInfo(fileSystem.Path);
                    foreach (var fileInfo in directoryInfo.GetFiles())
                    {
                        var fileName = fileInfo.Name;
                        foreach (var indexFile in request.VirtualHost.DefaultIndex)
                        {
                            if (fileName.Contains(indexFile.FileName))
                            {
                                return new Ok(FileHandler.MakeFromFile(fileInfo), MimeType.GetByExtension(fileInfo.Extension));
                            }
                        }
                    }
                }
                return new NotFound();
            }
            return NextHandler != null ? NextHandler.Handle(request) : new NotImplemented();
        }
    }
}
