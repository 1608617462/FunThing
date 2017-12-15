using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FunThing.Models
{
    public class LoginModel
    {
        [Key]
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "请输入用户名")]
        public string Username { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "请输入密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "记住我")]
        public bool RememberMe { get; set; }
        /*
                [Description("验证码")]
                public string ValidateCode { get; set; }*/
    }
    public class UserSignupModel
    {
        [Key]
        [Required(ErrorMessage = "用户名不能为空")]
        [DisplayName("用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "昵称不能为空")]
        [DisplayName("昵称")]
        public string NickName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "密码不能为空")]
        [MinLength(6, ErrorMessage = "{0}不能小于{1}位")]
        [DisplayName("密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "两次输入的密码不一致")]
        [DisplayName("确认密码")]
        public string ConfirmPassword { get; set; }

        [DisplayName("手机号码")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [DisplayName("邮箱")]
        public string Email { get; set; }
    }
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