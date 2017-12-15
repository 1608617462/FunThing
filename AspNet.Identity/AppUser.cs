using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;

namespace AspNet.Identity
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
    }
}