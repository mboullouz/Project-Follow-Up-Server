using Newtonsoft.Json;
using PUp.Models;
using PUp.Models.Repository;
using PUp.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using PUp.Services;
using PUp.Helpers;

namespace PUp.Controllers
{
    [App_Start.MyBasicAuth]
    [Authorize]
    public class NotificationApiController : ApiController
    {
        public NotificationService NotificationService { get; set; }


        public void Init()
        {
            var email = RequestContext.Principal.Identity.Name;
            NotificationService = new NotificationService(email, new Models.ModelStateWrapper(new Models.ValidationMessageHolder(), ModelState));
        }


        [HttpGet]
        public HttpResponseMessage All()
        {
            Init();
            return this.CreateJsonResponse(AppJsonUtil<List<NotificationDto>>.ToJson(NotificationService.AllForCurrentUser()));
        }
 
        [HttpGet]
        public HttpResponseMessage SeenUnseen(int id)
        {
            Init();
            return this.CreateJsonResponse(NotificationService.SeenUnseen(id).ToJson());
        }
    }
}
