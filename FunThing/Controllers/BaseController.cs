using FunThing.Class;
using FunThing.Models;
using Microsoft.AspNet.Identity.Owin;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FunThing.Controllers
{
    public partial class BaseController : Controller
    {
        public string _connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public AppUser IdentityUser;
        public static readonly Logger Log = LogManager.GetCurrentClassLogger();
        protected AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
        protected AppUser CurrentUser
        {
            get
            {
                if(IdentityUser==null)
                {
                    IdentityUser = UserManager.FindByNameAsync(HttpContext.User.Identity.Name).Result;
                }
                return IdentityUser;
            }
        }
        protected string CurrentIP
        {
            get
            {
                string clientIP = string.Empty;
                if (Request.Cookies.AllKeys.Contains("KOAL_CLIENT_IP"))
                {
                    clientIP = Server.UrlDecode(Request.Cookies["KOAL_CLIENT_IP"].Value);
                }
                else
                {
                    clientIP = WebHelper.GetRealIP();
                }
                return clientIP;
            }
        }
    }
}