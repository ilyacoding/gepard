using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Data
{
    public interface IHttpMessage
    {
        Dictionary<string, string> fields { get; set; }
    }
}
