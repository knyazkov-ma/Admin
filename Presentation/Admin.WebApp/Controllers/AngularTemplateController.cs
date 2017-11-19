using Admin.DataService.Interface;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Admin.Common;
using Admin.DataService.DTO;
using Admin.WebApp.Filters;
using System.Collections.Generic;
using Admin.WebApp.Resources;
using System.Threading;
using Admin.WebApp.Models;
using Admin.Entity;

namespace Admin.WebApp.Controllers
{
    //Layout of the angular application.
    [Authorize]
    
    public class AngularTemplateController : Controller
    {
        private readonly IWorkerUserService userService;
        

        public AngularTemplateController(IWorkerUserService userService)
        {
            this.userService = userService;
            
        }

        

        public WorkerUserDTO CurrentUser
        {
            get
            {
                if (!User.Identity.IsAuthenticated)
                    return null;

                if (Session[AppConstants.CURRENT_APPLICATION_USER_SESSION_KEY] == null)
                {
                    WorkerUserDTO currentUser = userService.GetDTO(User.Identity.GetUserId<long>());
                    Session[AppConstants.CURRENT_APPLICATION_USER_SESSION_KEY] = currentUser;
                }

                return (WorkerUserDTO)Session[AppConstants.CURRENT_APPLICATION_USER_SESSION_KEY];
            }
        }

        #region layout
        
        [HttpGet]
        [Culture]
        public ActionResult Index()
        {
            ViewBag.Menu = new Dictionary<string, string>()
            {
                {"users", Resource.Menu_Users}
            };
            
            return View("~/App/Main/views/layout/layout.cshtml",
                    new AppLayoutModel()
                    );
        }

        [HttpGet]
        [Culture]
        public ActionResult Header()
        {
            return PartialView("~/App/Main/views/layout/header.cshtml"); 
        }

        #endregion layout



        #region Users
        [HttpGet]
        public ActionResult Users()
        {
            return PartialView("~/App/Main/views/users/list.cshtml");
        }

        [HttpGet]
        public ActionResult UserCard()
        {
            return PartialView("~/App/Main/views/users/card.cshtml");
        }
        #endregion Users
        
    }
}