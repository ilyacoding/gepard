using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Core.HttpAction
{
    public interface IHttpAction
    {
        int Code { get; set; }
        object Object { get; set; }
        
    }
}
