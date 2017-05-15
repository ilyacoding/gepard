using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Core.HttpAction
{
    public class NotFound : IHttpAction
    {
        public int Code { get; set; }
        public object Object { get; set; }

        public NotFound()
        {
            Code = 404;
            Object = null;
        }
    }
}
