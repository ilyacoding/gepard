﻿using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Gepard.Core;
using Gepard.Core.HttpAction;
using Gepard.Core.HttpFields;
using Gepard.Core.Main;
using Gepard.Core.Requests;

namespace Gepard.Controllers
{
    public class DigestAuthHandler : IRequestHandler
    {
        public IRequestHandler NextHandler { get; set; }

        private Random Random { get; }
        private string Nonce { get; }

        public DigestAuthHandler()
        {
            Random = new Random(DateTime.Now.Millisecond);
            Nonce = RandomString(DateTime.Now.Second);
        }

        public IHttpAction Handle(Request request)
        {
            if (request.VirtualHost.DigestAuthConfigs != null)
            {
                foreach (var authConfig in request.VirtualHost.DigestAuthConfigs)
                {
                    if (request.Object.Uri.Url.StartsWith(authConfig.AuthDirectory))
                    {
                        // Need auth

                        var ha1 = Md5($"{authConfig.UserName}:{authConfig.Realm}:{authConfig.Password}");
                        var ha2 = Md5($"{request.Object.Method}:/{request.Object.Uri.Url}");
                        var response = Md5($"{ha1}:{Nonce}:{ha2}");

                        if (request.Object.Authorization.AuthType == "Digest" 
                            && request.Object.Authorization["UserName"] == authConfig.UserName 
                            && request.Object.Authorization["Nonce"] == Nonce 
                            && request.Object.Authorization["Realm"] == authConfig.Realm
                            && request.Object.Authorization["Response"] == response
                            && request.Object.Authorization["Uri"] == "/" + request.Object.Uri.Url)
                        {
                            return NextHandler != null ? NextHandler.Handle(request) : new NotImplemented();
                        }
                        else
                        {
                            var httpHeaders = new HttpHeaders();
                            httpHeaders.Add("WWW-Authenticate", $"Digest realm=\"{authConfig.Realm}\", nonce=\"{Nonce}\"");
                            return new Unauthorized(httpHeaders, null);
                        }
                    }
                }
            }
            return NextHandler != null ? NextHandler.Handle(request) : new NotImplemented();
        }
        
        private static string Md5(string input)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);
            
            var sb = new StringBuilder();

            foreach (var byteHash in hash)
            {
                sb.Append(byteHash.ToString("X2"));
            }

            return sb.ToString().ToLower();
        }

        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[Random.Next(s.Length)]).ToArray()).ToLower();
        }
    }
}
