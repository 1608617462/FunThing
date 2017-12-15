using FunThing.Class;
using FunThing.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using MySql.Data.Entity;
using Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace FunThing
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<AppIdentityDbContext>(AppIdentityDbContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppSignInManager>(AppSignInManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            //createRoleSandUsers();

        }
        
        public void createRoleSandUsers()
        {
            AppIdentityDbContext context = new AppIdentityDbContext();

            //TODO:个人理解，IdentityRole本身就存在。但是如果要增加自定义内容的话就继承此接口拓展。
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<AppUser>(new UserStore<AppUser>(context));
            //系统运行之前检查Admin角色是否存在，如果不存在就新建
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                //TODO:异步方法是否需要增加判断是否已经完成
                roleManager.Create(role);

                var user = new AppUser();
                user.UserName = "1608617462@QQ.com";
                user.Email = "1608617462@QQ.com";
                user.Nickname = "系统管理员";

                string userPWD = "17693lyl";
                var U_Result = UserManager.CreateAsync(user, userPWD).Result;
                if(U_Result.Succeeded)
                {
                    var R_Result = UserManager.AddToRoleAsync(user.Id, "Admin").Result;
                }
            }
    }
        // 配置要在此应用程序中使用的应用程序登录管理器。
    }
    public class AppSignInManager : SignInManager<AppUser, string>
    {
        public AppSignInManager(AppUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public static AppSignInManager Create(IdentityFactoryOptions<AppSignInManager> options, IOwinContext context)
        {
            return new AppSignInManager(context.GetUserManager<AppUserManager>(), context.Authentication);
        }
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(AppUser user)
        {
            return user.GenerateUserIdentityAsync((AppUserManager)UserManager);
        }
    }
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) : base(store)
        {

        }
        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            AppIdentityDbContext db = context.Get<AppIdentityDbContext>();

            AppUserManager manager = new AppUserManager(new UserStore<AppUser>(db));
            return manager;
        }
    }


    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext() : base("DefaultConnection")
        {
        }
        static AppIdentityDbContext()
        {
            Database.SetInitializer(new MySqlInitializer());
        }
        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }
    }
    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<AppIdentityDbContext>
    {
        protected override void Seed(AppIdentityDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }
        public void PerformInitialSetup(AppIdentityDbContext context)
        {
            //初始化
        }
    }
}