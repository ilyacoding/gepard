using System;

namespace Gepard.Core.HttpFields
{
    public class HttpVersion
    {
        public Version Version { get; set; }

        public HttpVersion()
        {
            Version = new Version(1, 1);
        }

        public override string ToString()
        {
            return "HTTP/" + Version;
        }
    }
}
