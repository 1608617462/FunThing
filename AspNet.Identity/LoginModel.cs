using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNet.Identity
{
   public class LoginModel
    {
        [Description("用户名")]
        public string Username { get; set; }

        [Description("密码")]
        public string Password { get; set; }

        [Description("验证码")]
        public string ValidateCode { get; set; }
    }
}
