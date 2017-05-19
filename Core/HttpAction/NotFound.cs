using Gepard.Core.HttpFields;
using Gepard.Core.Main;
using Gepard.Core.Responses;

namespace Gepard.Core.HttpAction
{
    public class NotFound : IHttpAction
    {
        public HttpResponse HttpResponse { get; set; }

        public NotFound(bool includeBody = true)
        {
            HttpResponse = new HttpResponse
            {
                HttpStatusCode = 404,
                IsSuccessStatus = false,
                Content = new HttpContent() { Data = null, IncludeBody = includeBody },
            };
        }
    }
}
