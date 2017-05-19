using Gepard.Core.HttpFields;
using Gepard.Core.Main;
using Gepard.Core.Responses;

namespace Gepard.Core.HttpAction
{
    public class Ok : IHttpAction
    {
        public HttpResponse HttpResponse { get; set; }

        public Ok(HttpHeaders httpHeaders, byte[] dataBytes, bool includeBody = true)
        {
            HttpResponse = new HttpResponse
            {
                HttpStatusCode = 200,
                IsSuccessStatus = true,
                Content = new HttpContent() { Data = dataBytes, IncludeBody = includeBody },
                Headers = httpHeaders
            };
        }
    }
}
