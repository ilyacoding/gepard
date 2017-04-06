﻿using System.Collections.Generic;
using System.Linq;

namespace Gepard
{
    public static class ResponseStatus
    {
        private static readonly Dictionary<int, string> CodeDictionary = new Dictionary<int, string>()
        {
            { 501, "Not implemented" },
            { 200, "OK" },
            { 400, "Bad request" },
            { 401, "Unauthorized" },
            { 403, "Forbidden" },
            { 404, "Not found" },
            { 405, "Method not allowed" },
            { 500, "Internal Server Error" }
        };
        public static string Get(int code) => CodeDictionary.ContainsKey(code) ? $"{code} {CodeDictionary[code]}" : $"{CodeDictionary.First().Key} {CodeDictionary.First().Value}";
    }
}