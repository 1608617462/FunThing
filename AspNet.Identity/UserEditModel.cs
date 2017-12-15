using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNet.Identity
{
    public class UserEditModel
    {
        //TODO:编辑model待补充
        [Description("主键")]
        public string Id { get; set; }
        [Description("用户名")]
        public string Username { get; set; }
        [Description("密码")]
        public string Password { get; set; }
        [Description("邮箱")]
        public string Email { get; set; }

        [Description("手机号码")]
        public string Phonenumber { get; set; }

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
