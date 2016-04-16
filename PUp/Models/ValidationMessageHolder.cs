using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models
{
    public class ValidationMessageHolder
    {
        /// <summary>
        /// Gather errors
        /// </summary>
        public List<SimpleKeyValue<string, string>> ErrorMessages { get; }
 
        /// <summary>
        /// Let the user know about successful operations!
        /// </summary>
        public List<SimpleKeyValue<string, string>> SuccessMessages { get; set; }
 
        /// <summary>
        /// General message a kind of summary!
        /// </summary>
        public string Message { get; set; }
        

        public ValidationMessageHolder(int state=1, string message="Model is valid")
        {
            ErrorMessages = new List<SimpleKeyValue<string, string>>();
            SuccessMessages = new List<SimpleKeyValue<string, string>>();
            State = state;
            Message = message;
        }

        

        public ValidationMessageHolder AddError(string key,string msg)
        {
            ErrorMessages.Add(new SimpleKeyValue<string, string> { Key = key, Value = msg });
            if (ErrorMessages.Count() > 0) State = 0;
            Message = "Model is not valid";
            return this;
        }

        public ValidationMessageHolder AddSuccess(string key, string msg)
        {
            SuccessMessages.Add(new SimpleKeyValue<string,string> { Key=key, Value=msg });
            return this;
        }

        public ValidationMessageHolder Clear()
        {
            ErrorMessages.Clear();
            State = 1;
            Message = "Model is valid";
            return this;
        }

        public bool IsValid()
        {
            if (ErrorMessages.Count() > 0) State = 0;
            return Message.Count() > 0;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
        }

        //Additional field intended to be used by the result Json in the client
        // If Valid State = 1  else 0
        //So need to be in sync with the state of validation 
        public int State { get; set; }
    }

     
}