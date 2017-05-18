using System.Collections.Generic;
using System.Linq;

namespace Gepard
{
    public static class HttpResponseStatus
    {
        private static readonly Dictionary<int, string> CodeDictionary = new Dictionary<int, string>()
        {
            { 501, "Not implemented" },

            { 200, "OK" },
            { 201, "Created" },
            { 304, "Not modified" },
            { 400, "Bad request" },
            { 401, "Unauthorized" },
            { 403, "Forbidden" },
            { 404, "Not found" },
            { 405, "Method not allowed" },
            { 415, "Unsupported media type" },
            { 434, "Requested host unavailable" },
            { 500, "Internal Server Error" }
        };
        public static string Get(int code) => CodeDictionary.ContainsKey(code) ? $"{code} {CodeDictionary[code]}" : $"{CodeDictionary.First().Key} {CodeDictionary.First().Value}";
    }
}
