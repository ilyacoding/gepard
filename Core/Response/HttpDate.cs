using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Core.Response
{
    public class HttpDate
    {
        public DateTime DateTime { get; set; }

        public HttpDate(DateTime dateTime)
        {
            DateTime = dateTime;
        }

        public override string ToString()
        {
            return DateTime.ToUniversalTime().ToString("r");
        }
    }
}
