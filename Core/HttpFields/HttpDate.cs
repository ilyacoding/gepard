using System;

namespace Gepard.Core.HttpFields
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
