using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Configuration.Server;

namespace Gepard.Core
{
    public interface IModel
    {
        ServerConfig ServerConfig { get; set; }
        Response Execute(Request request);
    }
}
