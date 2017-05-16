using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Core;
using Gepard.Core.HttpAction;

namespace Gepard.Controllers
{
    public class DigestAuthHandler : IRequestHandler
    {
        public IRequestHandler NextHandler { get; set; }

        public IHttpAction Handle(Request request)
        {
            throw new NotImplementedException();
        }
    }
}
