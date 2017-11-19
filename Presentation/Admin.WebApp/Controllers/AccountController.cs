﻿using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Admin.WebApp.Identity;
using Admin.WebApp.Models;
using Admin.DataService.Interface;
using Admin.Common;
using Admin.DataService.Common;
using System.Collections.Generic;
using Admin.WebApp.Filters;
using Admin.WebApp.Resources;
using Admin.DataService.DTO;
using System.Net.Mail;
using System;
using System.Net.Configuration;
using System.Configuration;
using log4net;

namespace Admin.WebApp.Controllers
{
    [Authorize]
    [Culture]
    public class AccountController : Controller
    {
        private readonly ILog log = LogManager.GetLogger("Admin.WebApp");
        private readonly IWorkerUserService userService;
        
        public AccountController(IWorkerUserService userService)
        {
            this.userService = userService;
        }
                
        private ApplicationSignInManager signInManager;
        
        
        public ApplicationSignInManager SignInManager
        {
            get
            {
                ApplicationSignInManager m = signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
                return m;
            }
            private set 
            { 
                signInManager = value; 
            }
        }
        
        
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            
            // Сбои при входе не приводят к блокированию учетной записи
            // Чтобы ошибки при вводе пароля инициировали блокирование учетной записи, замените на shouldLockout: true

            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            
            switch (result)
            {
                case SignInStatus.Success:
                    var u = userService.GetDTO(model.UserName);
                    Session[AppConstants.CURRENT_APPLICATION_USER_SESSION_KEY] = u;
                    return RedirectToLocal(returnUrl);
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", Resource.Message_LoginError);
                    return View(model);
            }
        }

        
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session[AppConstants.CURRENT_APPLICATION_USER_SESSION_KEY] = null;
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult RecoveryPassword()
        {
            return View(new RecoveryPassvordViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult RecoveryPassword(RecoveryPassvordViewModel model)
        {
            if (ModelState.IsValid)
            {
                WorkerUserDTO user = userService.GetDTO(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", Resource.Message_EmailNotRegitered);
                }
                else
                {
                    Task task = new Task(() => sendEmail(model.Email,
                        String.Format(Resource.Text_RecoveryPasswordSubject, Resource.AppName),
                        user.Password));
                    task.Start();

                    model.IsSend = true;
                }

            }

            return View(model);
        }


        protected void sendEmail(string email, string subject, string body)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            SmtpClient smtpClient = new SmtpClient();

            try
            {
                smtpClient.Send(new MailMessage(smtpSection.From, email, subject, body));
            }
            catch(Exception ex)
            {
                log.Error("Application error", ex);
            }
            
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                
                if (signInManager != null)
                {
                    signInManager.Dispose();
                    signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region private

        private void addErrors(DataServiceException ex)
        {
            foreach (string key in ex.DataServiceExceptionData.Messages.Keys)
            {
                IList<string> messages = ex.DataServiceExceptionData.Messages[key];
                foreach(string message in messages)
                    ModelState.AddModelError(key, message);
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "AngularTemplate");
        }

        #endregion
    }
}