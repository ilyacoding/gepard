using Gepard.Core.HttpFields;
using Gepard.Core.Main;
using Gepard.Core.Responses;

namespace Gepard.Core.HttpAction
{
    public class NotSatisfiable : IHttpAction
    {
        public HttpResponse HttpResponse { get; set; }

        public NotSatisfiable(bool includeBody = true)
        {
            HttpResponse = new HttpResponse
            {
                HttpStatusCode = 416,
                IsSuccessStatus = false,
                Content = new HttpContent() { Data = null, IncludeBody = includeBody },
            };
        }
    }
}
