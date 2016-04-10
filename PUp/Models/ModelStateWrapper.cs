using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;

namespace PUp.Models
{   

    /// <summary>
    /// This class hold state of a Request: all infomation about validations (in case there is some) then send them to the User
    /// It extracts Errors/Values from ModelStateDictionary 
    /// </summary>
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

        public void AddSuccess(string key, string successMessage)
        {
            validationMessageHolder.AddSuccess(key, successMessage);
        }
         //get state of messagenholder that already got init by ModelState
        public bool IsValid()
        {
            initMessages();
            return validationMessageHolder.IsValid();
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
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }

                }

            }
        }
    }
}
