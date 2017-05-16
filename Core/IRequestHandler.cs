using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Core.HttpAction;

namespace Gepard.Core
{
    public interface IRequestHandler
    {
        IHttpAction Handle(Request request);
        IRequestHandler NextHandler { get; set; }
    }
}
