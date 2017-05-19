using Gepard.Core.Responses;

namespace Gepard.Core.Main
{
    public interface IHttpAction
    {
        HttpResponse HttpResponse { get; set; }
    }
}
