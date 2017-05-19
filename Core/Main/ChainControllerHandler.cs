using Gepard.Core.HttpAction;

namespace Gepard.Core.Main
{
    public class ChainControllerHandler
    {
        private IRequestHandler LastRequestHandler { get; set; }
        private IRequestHandler RequestHandler { get; set; }
      
        public IHttpAction Execute(Requests.Request request)
        {
            return RequestHandler.Handle(request);
        }

        public void Reg(IRequestHandler requestHandler)
        {
            if (RequestHandler == null)
            {
                RequestHandler = requestHandler;
                LastRequestHandler = requestHandler;
                return;
            }

            LastRequestHandler.NextHandler = requestHandler;
            LastRequestHandler = requestHandler;
        }
    }
}
