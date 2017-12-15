using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AspNet.Identity
{
    //自定义密码验证规则
    class CustomPasswordValidator:PasswordValidator
    {
        public override async Task<IdentityResult> ValidateAsync(string password)
        {
            IdentityResult result = await base.ValidateAsync(password);
            List<string> errors = result.Errors.ToList();
            Regex letter = new Regex("[A-Za-z]+");//判断是否包含数字
            if (password.Contains("12345"))
            {
                //IdentityResult.Errors是只读的,无法直接赋值，只能通过实例化IdentityResult类并通过构造函数传入Errors
                errors.Add("密码不能包含连续数字");
            }
            if(!letter.IsMatch(password))
            {
                errors.Add("密码必须包含字母");
            }
                result = new IdentityResult(errors);
            return result;
        }
    }
}
