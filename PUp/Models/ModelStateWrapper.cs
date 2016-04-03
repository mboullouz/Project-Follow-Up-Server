using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;

namespace PUp.Models
{
    public class ModelStateWrapper
    {
        private ValidationMessageHolder validationMessageHolder;
        private System.Web.Http.ModelBinding.ModelStateDictionary modelState;
 

        public ModelStateWrapper(ValidationMessageHolder validationMessageWrapper, System.Web.Http.ModelBinding.ModelStateDictionary modelState)
        {
            this.validationMessageHolder = validationMessageWrapper;
            this.modelState = modelState;
        }

         

        public void AddError(string key, string errorMessage)
        {
            validationMessageHolder.Add(key, errorMessage);
        }
        
        public bool IsValid()
        {
            initMessages();
            return  validationMessageHolder.IsValid();
        }

        public string ToJson()
        {
            return validationMessageHolder.ToJson();
        }

        private void initMessages()
        {
           
            foreach (var v in modelState.Values)
            {
                foreach (var e in v.Errors)
                {
                    try  //avoid error with the same -key
                    {
                        validationMessageHolder.Add(v.Value.AttemptedValue, e.ErrorMessage);
                    }
                    catch (Exception)
                    {

                       //Some duplication
                    }
                   
                }

            }
        }
    }
}