using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNet.Identity
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) : base(store)
        {

        }
        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options,
        IOwinContext context)
        {
            AppIdentityDbContext db = context.Get<AppIdentityDbContext>();
            AppUserManager manager = new AppUserManager(new UserStore<AppUser>(db));
            manager.PasswordValidator = new CustomPasswordValidator
            {
                RequiredLength = 8,
                RequireDigit = true,
            };
            manager.UserValidator = new CustomUserValidator(manager)
            {
                //用户名只能包含数字和字母
                AllowOnlyAlphanumericUserNames = false,
                //Email地址必须唯一
                RequireUniqueEmail = true
            };
            return manager;
        }

    }
}