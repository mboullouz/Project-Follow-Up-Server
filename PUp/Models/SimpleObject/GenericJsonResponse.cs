using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.SimpleObject
{
    public class GenericJsonResponse
    {
        public string Message { get; set; }
        public string State { get; set; }
        public bool Success { get; set; }
        public int  IdEntity { get; set; }

        public override string ToString()
        {
            var str = "{ idEntity:"+IdEntity+", success:" + Success.ToString() + ",state:" + State + ", message:" + Message + "}";
            return str;
        }
    }
}