using Gepard.Core.HttpFields;
using Gepard.Core.Main;
using Gepard.Core.Responses;

namespace Gepard.Core.HttpAction
{
    public class BadRequest : IHttpAction
    {
        public HttpResponse HttpResponse { get; set; }

        public BadRequest(bool includeBody = true)
        {
            HttpResponse = new HttpResponse
            {
                HttpStatusCode = 400,
                IsSuccessStatus = false,
                Content = new HttpContent() { Data = null, IncludeBody = includeBody },
            };
        }
    }
}
