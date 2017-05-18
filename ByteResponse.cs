using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard
{
    public class ByteResponse
    {
        public byte[] ArrayBytes { get; set; }
        public bool ConnectionAlive { get; set; }

        public ByteResponse(byte[] arrayBytes, bool conntectionAlive)
        {
            ArrayBytes = arrayBytes;
            ConnectionAlive = conntectionAlive;
        }
    }
}
