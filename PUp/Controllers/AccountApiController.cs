using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using PUp.Models;
using PUp.Models.Entity;
using PUp.Models.Dto;
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

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountApiController : ApiController
    {
        private ModelStateWrapper modelStateWrapper;
        internal void Init()
        {
            modelStateWrapper = new ModelStateWrapper(new ValidationMessageHolder(), ModelState);
        }
        /// <summary>
        /// Check a login model that contais username Or email, password and rememberMe
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public NegotiatedContentResult<string> CheckCredentials(AuthObject model)
        {
            Init();
            var UserManager = new UserManager<UserEntity>(new UserStore<UserEntity>(new DatabaseContext()));
            UserEntity user = null;
            try
            { 
                user = UserManager.Find(model.Username, model.Password);
            }
            catch (Exception e)
            {
                modelStateWrapper.AddError("Exception", e.ToString());
                return Content(HttpStatusCode.Forbidden, "0");
            }
            var userDto = new UserDto(user);
            return Content(HttpStatusCode.OK, user == null ? "0" : userDto.ToJson());
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
            Init();
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

                    return Content(HttpStatusCode.OK, modelStateWrapper.ToJson());
                }

                var errorCounter = 0;
                foreach (var e in result.Errors)
                {
                    modelStateWrapper.AddError("ModelState:" + errorCounter++, e);
                }
            }
            else
            {
                var errorCounter = 0;
                foreach (var v in ModelState.Values)
                {
                    foreach (var e in v.Errors)
                    {
                        modelStateWrapper.AddError("ModelState:" + errorCounter++, e.ErrorMessage);
                    }

                }
            }

            // Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
            return Content(HttpStatusCode.NotAcceptable, modelStateWrapper.ToJson());
        }

    }
}