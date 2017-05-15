using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Controllers
{
    public abstract class RequestHandler
    {
        public abstract void Handle(Request request, Response response);
        public RequestHandler NextHandler { get; set; }
    }
}
