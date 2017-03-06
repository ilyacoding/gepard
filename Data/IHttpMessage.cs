using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Data
{
    public interface IHttpMessage
    {
        string Connection { get; set; }
        string Content { get; set; }
    }
}
