namespace Gepard.Core.Main
{
    public abstract class RequestHandler
    {
        public RequestHandler NextHandler { get; set; }
        public abstract IHttpAction Handle(Requests.Request request);

        public void SetHandler(RequestHandler requestHandler)
        {
            if (NextHandler != null)
            {
                NextHandler.SetHandler(requestHandler);
            }
            else
            {
                NextHandler = requestHandler;
            }
        }
    }
}
