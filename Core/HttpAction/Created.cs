﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Core.HttpAction
{
    public class Created : IHttpAction
    {
        public int Code { get; set; }
        public object Object { get; set; }

        public Created(object obj)
        {
            Code = 201;
            Object = obj;
        }
    }
}
