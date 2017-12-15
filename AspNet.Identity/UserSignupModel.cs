using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNet.Identity
{
    public class UserSignupModel
    {
        [Key]
        [Required(ErrorMessage ="用户名不能为空")]
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
        [Compare("Password",ErrorMessage ="两次输入的密码不一致")]
        [DisplayName("确认密码")]
        public string ConfirmPassword { get; set; }

        [DisplayName("手机号码")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [DisplayName("邮箱")]
        public string Email { get; set; }
    }
}