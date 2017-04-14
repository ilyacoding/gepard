using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Configuration.Server;

namespace Gepard.Controllers
{
    public class PostController : IController
    {
        public ServerConfig ServerConfig { get; set; }

        public Response Execute(Request request)
        {
            throw new NotImplementedException();
        }
    }
}
