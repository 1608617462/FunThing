using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace FunThing.Models
{
    public class AppUser:IdentityUser
    {
        [Description("昵称")]
        public string Nickname { get; set; }

        [Description("身份证号")]
        public string Idcard { get; set; }

        [Description("姓名")]
        public string RealName { get; set; }

        [Description("性别代码")]
        public string Gender_Code { get; set; }

        [Description("性别")]
        public string Gender_Text { get; set; }

        [Description("民族代码")]
        public string Nation_Code { get; set; }

        [Description("民族")]
        public string Nation_Text { get; set; }

        [Description("删除标志")]
        public string DeleteMark { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }
}