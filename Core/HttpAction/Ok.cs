using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Core.HttpAction
{
    public class Ok : IHttpAction
    {
        public int Code { get; set; }
        public object Object { get; set; }

        public Ok(object obj)
        {
            Code = 200;
            Object = obj;
        }
    }
}
