using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FunThing.Controllers
{
    public class FrameController : BaseController
    { 
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult LoginPartial()
        {
            ViewBag.User = CurrentUser;
            return PartialView();
        }
    }
}