using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Gepard.Configuration.Server;
using Gepard.Core;
using Gepard.Core.FileHandling;
using Gepard.Core.HttpAction;
using Gepard.Core.Request;
using Gepard.Core.Response;

namespace Gepard.Controllers
{
    public class GetHeadMethodHandler : IRequestHandler
    {
        private string DirectoryRoot { get; set; }
        public IRequestHandler NextHandler { get; set; }

        public GetHeadMethodHandler(string directoryRoot)
        {
            DirectoryRoot = directoryRoot;
        }

        public IHttpAction Handle(HttpRequest request)
        {
            if (request.Object.Method == "GET" || request.Object.Method == "HEAD")
            {
                var includeBody = request.Object.Method != "HEAD";

                var httpHeaders = new HttpHeaders();

                var fileSystem = new FileSystem(Path.Combine(DirectoryRoot, "www", request.VirtualHost.Directory, request.Object.Uri.Url), request.VirtualHost.DefaultIndex);

                var fileDescription = fileSystem.GetFile();

                if (fileDescription == null) return new NotFound(includeBody);

                var dateChange = new HttpDate(fileDescription.GetLastModified());

                httpHeaders.Add("Content-Type", HttpMimeType.GetByExtension(fileDescription.GetExtension()) + "; charset=" + fileDescription.GetEncoding());
                httpHeaders.Add("Last-Modified", dateChange.ToString());

                if (request.Object.HttpRange != null)
                {
                    try
                    {
                        request.Object.HttpRange.Normalize(fileDescription.GetFileSize() - 1);
                        var bytesArray = fileDescription.GetRangeBytes(request.Object.HttpRange);
                        if (bytesArray.Length > 0)
                        {
                            httpHeaders.Add("Content-Range", request.Object.HttpRange.ToString());
                            return new PartialContent(httpHeaders, bytesArray, includeBody);
                        }
                        else
                        {
                            return new NotSatisfiable(includeBody);
                        }
                    }
                    catch (Exception)
                    {
                        request.Object.HttpRange = null;
                    }
                }

                if (request.Object["If-Modified-Since"] != null)
                {
                    try
                    {
                        var requestDate = DateTime.Parse(request.Object["If-Modified-Since"].Trim());
                        if (requestDate >= dateChange.DateTime)
                        {
                            return new NotModified(httpHeaders, includeBody);
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                }

                return new Ok(httpHeaders, fileDescription.GetAllBytes(), includeBody);
            }
            return NextHandler != null ? NextHandler.Handle(request) : new NotImplemented();
        }
    }
}
