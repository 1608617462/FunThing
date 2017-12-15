using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AspNet.Identity
{
    class CustomUserValidator:UserValidator<AppUser>
    {
        public CustomUserValidator(AppUserManager mgr):base(mgr)
        {

        }
        public override async Task<IdentityResult> ValidateAsync(AppUser user)
        {
            IdentityResult result = await base.ValidateAsync(user);
            if (user.Email.ToLower().EndsWith("@gmail.com"))
            {
                List<string> errors = result.Errors.ToList();
                errors.Add("暂不支持谷歌邮箱");
                result = new IdentityResult(errors);
            }
            return result;
        }

    }
}
