namespace Gepard.Core.Main
{
    public interface IRequestHandler
    {
        IRequestHandler NextHandler { get; set; }
        IHttpAction Handle(Requests.Request request);
    }
}
