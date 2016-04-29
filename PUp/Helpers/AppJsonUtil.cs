using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Helpers
{
   
    public  class AppJsonUtil<T>
    {
        public static string ToJson(T obj)
        {
            return JsonConvert.SerializeObject(obj,
             Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        public static T FromJson(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}