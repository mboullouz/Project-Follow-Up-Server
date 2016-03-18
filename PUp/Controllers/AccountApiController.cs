using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using PUp.Models;
using PUp.Models.Entity;
using PUp.Models.SimpleObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace PUp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountApiController : ApiController
    {

        /// <summary>
        /// Check a login model that contais username Or email, password and rememberMe
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public NegotiatedContentResult<string> CheckCredentials(AuthObject model)
        {

            var UserManager = new UserManager<UserEntity>(new UserStore<UserEntity>(new DatabaseContext()));
            UserEntity user=null;
            try {//model may = or contain null
               user= UserManager.Find(model.Username, model.Password);
            }
            catch(Exception e)
            {
                return Content(HttpStatusCode.Forbidden, "0");
            }
             
            return Content(HttpStatusCode.OK, user==null?"0":"1");
        }


        /// <summary>
        /// Verify that a user have a valid auth hash, the server will respond with 1
        /// else send an HTML page asking the user to login
        /// </summary>
        /// <returns></returns>
        [App_Start.MyBasicAuth]
        [Authorize]
        [HttpPost]
        public NegotiatedContentResult<string> Verify()
        {
            return Content(HttpStatusCode.OK, "1");
        }

    }
}