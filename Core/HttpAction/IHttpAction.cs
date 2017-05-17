using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Core.Response;

namespace Gepard.Core.HttpAction
{
    public interface IHttpAction
    {
        HttpResponse HttpResponse { get; set; }
    }
}
