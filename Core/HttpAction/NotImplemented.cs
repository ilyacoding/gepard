using Gepard.Core.HttpFields;
using Gepard.Core.Main;
using Gepard.Core.Responses;

namespace Gepard.Core.HttpAction
{
    public class NotImplemented : IHttpAction
    {
        public HttpResponse HttpResponse { get; set; }

        public NotImplemented(bool includeBody = true)
        {
            HttpResponse = new HttpResponse
            {
                HttpStatusCode = 501,
                IsSuccessStatus = false,
                Content = new HttpContent() { Data = null, IncludeBody = includeBody },
            };
        }

    }
}
