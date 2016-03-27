using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.ViewModels
{
    public abstract  class BaseModelView
    {
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this,
             Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }
    }
}