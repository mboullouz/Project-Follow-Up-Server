using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace PUp.Controllers
{
      public static class ControllerExtensions
    {
        public static System.Net.Http.HttpResponseMessage CreateJsonResponse(this System.Web.Http.ApiController ctrl, string jsonContent, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var response = ctrl.Request.CreateResponse(statusCode);
            response.Content = new StringContent(jsonContent, System.Text.Encoding.UTF8, Models.AppMediaType.APP_JSON);
            return response;
        }
    }
}