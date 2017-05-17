using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Core.HttpAction;
using Gepard.Core.Response;

namespace Gepard.Core
{
    public interface IRequestHandler
    {
        IRequestHandler NextHandler { get; set; }
        IHttpAction Handle(Request request);
    }
}
