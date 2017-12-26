using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using FunThing.Class;
using FunThing.Models;
using System;
using System.Diagnostics;
using NLog;
using NLog.Fluent;

namespace FunThing.Controllers
{
    /// <summary>
    /// 用户账户相关
    /// </summary>
    public class AccountController :BaseController
    {
        public AppSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<AppSignInManager>();
            }
        }
        //private AppRoleManager _roleManager;

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="model">用户注册Model</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SignUp(UserSignupModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.UserName, Nickname = model.NickName, PhoneNumber = model.PhoneNumber, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return Redirect("/Account/Login");
                }
                AddErrorsFromResult(result);
            }
             return View(model);
        }
        /// <summary>
        /// 将返回结果中的错误添加到错误集合中
        /// </summary>
        /// <param name="result">返回的结果</param>
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户的主键</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> DeleteUser(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                return View("Error", result.Errors);
            }
            return View("Error", new string[] { "此用户不存在" });
        }
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="eUser">用户编辑Model</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> EditUser(UserEditModel eUser)
        {
            AppUser user = await UserManager.FindByIdAsync(eUser.Id);
            if(user!=null)
            {
                IdentityResult validPass = null;
                if(!string.IsNullOrEmpty(eUser.Password))
                {
                    validPass = await UserManager.PasswordValidator.ValidateAsync(eUser.Password);
                    if(validPass.Succeeded)
                    {
                        user.PasswordHash = UserManager.PasswordHasher.HashPassword(eUser.Password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                user.Email = eUser.Email;
                IdentityResult validEmail = await UserManager.UserValidator.ValidateAsync(user);
                if(!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                else if(validPass.Succeeded)
                {
                    IdentityResult result = await UserManager.UpdateAsync(user);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("","此用户不存在");
            }
            return View(user);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if(HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect(returnUrl??"/Home/Index");
            }
            ViewBag.returnUrl = returnUrl??"/Home/Index";
            return View();
        }
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="model">用户登陆Model</param>
        /// <param name="returnUrl">登陆成功后跳转的页面</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [UserTrackerLog]
        public async Task<ActionResult> Login(LoginModel model,string returnUrl)
        {
            returnUrl = null;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await SignInManager.PasswordSignInAsync(model.Username,model.Password,model.RememberMe,shouldLockout:false);
            switch(result)
            {
                case SignInStatus.Success:

                    return Redirect(returnUrl??"/Home/Index");
                case SignInStatus.LockedOut:
                    ModelState.AddModelError("", "此用户已被锁定");
                    return View(model);
                default:
                    ModelState.AddModelError("", "用户名或密码错误");
                    return View(model);
            }
        }
        public ActionResult SignOut()
        {
            AuthManager.SignOut();
            return Redirect("/Home/Index");
        }
        private IAuthenticationManager AuthManager
        {
            //TODO:和直接使用HttpContext.GetOwinContext().Authentication.SignOut();的区别？
            get
            {
                //Authentication是AuthenticationManager的一个实现
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        /*
        [HttpPost]
        public async Task<ActionResult> CreateRole(string name)
        {
            if(ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new AppRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View();
        }

        public AppRoleManager roleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<AppRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        [HttpPost]
        public async Task<ActionResult>DeleteRole(string id)
        {
            AppRole role = await roleManager.FindByIdAsync(id);
            if(role!=null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error",result.Errors);
                }
            }
            else
            {
                return View("Error",new string[] {"无法找到该Role"});
            }
        }*/

    }
}