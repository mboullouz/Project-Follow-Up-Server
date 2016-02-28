using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PUp.Services
{
    public class ModelStateWrapper
    {
        private TempDataDictionary tempData;
        private ModelStateDictionary modelState;

        public ModelStateWrapper(TempDataDictionary tempData, ModelStateDictionary modelState)
        {
            this.tempData = tempData;
            this.modelState = modelState;
        }

        #region Flash message
        public void Flash(string message, FlashLevel level = FlashLevel.Info)
        {
            IList<string> messages = null;
            string key = String.Format("flash-{0}", level.ToString().ToLower());

            messages = (tempData.ContainsKey(key))
                ? (IList<string>)tempData[key]
                : new List<string>();

            messages.Add(message);

            tempData[key] = messages;
        }

        #endregion

        #region Model Validation
        public void AddError(string key, string errorMessage)
        {
            modelState.AddModelError(key, errorMessage);
        }
        public bool IsValid
        {
            get { return modelState.IsValid; }
        }
        #endregion
    }
    public enum FlashLevel
    {
        Success = 1,
        Info    = 2,
        Warning= 3,
        Danger = 4,
    }
}