using PUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Controllers 
{
    public static class FlashMessageHelper
    {
        public static void Flash(this System.Web.Mvc.Controller controller, string message, FlashLevel level)
        {
            IList<string> messages = null;
            string key = String.Format("flash-{0}", level.ToString().ToLower());

            messages = (controller.TempData.ContainsKey(key))
                ? (IList<string>)controller.TempData[key]
                : new List<string>();

            messages.Add(message);

            controller.TempData[key] = messages;
        }
    }

    public enum FlashLevel
    {
        Primary = 1,
        Success = 2,
        Info = 3,
        Warning = 4,
        Danger = 5,
        Error = 6
    }

}