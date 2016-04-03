﻿using Microsoft.AspNet.Identity;
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
            UserEntity user = null;
            try
            {//model may = or contain null
                user = UserManager.Find(model.Username, model.Password);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.Forbidden, "0");
            }

            return Content(HttpStatusCode.OK, user == null ? "0" : "1");
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


        [HttpPost]
        [AllowAnonymous]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<NegotiatedContentResult<string>> Register(ViewModels.Auth.RegisterViewModel model)
        {
            ValidationMessageHolder validationMessageHolder = new ValidationMessageHolder();
            if (ModelState.IsValid)
            {
                var user = new UserEntity { Name = model.Name, UserName = model.Email, Email = model.Email };
                var UserManager = new UserManager<UserEntity>(new UserStore<UserEntity>(new DatabaseContext()));
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // Pour plus d'informations sur l'activation de la confirmation du compte et la réinitialisation du mot de passe, consultez http://go.microsoft.com/fwlink/?LinkID=320771
                    // Envoyer un message électronique avec ce lien
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirmez votre compte", "Confirmez votre compte en cliquant <a href=\"" + callbackUrl + "\">ici</a>");

                    return Content(HttpStatusCode.OK, validationMessageHolder.ToJson());
                }

                var errorCounter = 0;
                foreach (var e in result.Errors)
                {
                    validationMessageHolder.Add("ModelState:" + errorCounter++, e);
                }

            }
            else
            {
                var errorCounter = 0;
                foreach (var v in ModelState.Values)
                {
                    foreach (var e in v.Errors)
                    {
                        validationMessageHolder.Add("ModelState:" + errorCounter++, e.ErrorMessage);
                    }
                        
                }
            }

            // Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
            return Content(HttpStatusCode.NotAcceptable, validationMessageHolder.ToJson());
        }

    }
}