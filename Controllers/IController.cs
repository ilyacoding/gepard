using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Gepard.Configuration.Server;

namespace Gepard.Controllers
{
    public interface IController
    {
        ServerConfig ServerConfig { get; set; }
        Response Execute(Request request);
    }
}
