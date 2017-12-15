using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FunThing.Class
{
    /// <summary>
    /// 记录当前Action访问者的UserName、Controller、时间。
    /// </summary>
    public class UserTrackerLogAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            var aDesctiption = filterContext.ActionDescriptor;
            string controllerName = aDesctiption.ControllerDescriptor.ControllerName;
            string actionName = aDesctiption.ActionName;
            string userName = filterContext.HttpContext.User.Identity.Name.ToString();
            var _params = filterContext.HttpContext.Request.Form;
            string url = filterContext.HttpContext.Request.Url == null ? string.Empty : HttpUtility.UrlDecode(filterContext.HttpContext.Request.Url.AbsoluteUri,Encoding.UTF8);
            string IP = HttpContext.Current.Request.Cookies.AllKeys.Contains("KOAL_CLIENT_IP") ? HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies["KOAL_CLIENT_IP"].Value): WebHelper.GetRealIP();
            StringBuilder sParams = new StringBuilder();
            StringBuilder message = new StringBuilder();
            message.Append("用户");
            message.Append(userName);
            message.Append("访问了");
            message.Append(controllerName);
            message.Append("/");
            message.Append(actionName);
            if (_params.Count != 0)
            {
                for (int i=0;i<_params.Count; i++)
                {
                    if (_params.Keys[i].ToUpper() != "PASSWORD" && _params.Keys[i].ToUpper() != "__REQUESTVERIFICATIONTOKEN")
                    {
                        sParams.Append("|");
                        sParams.Append(_params.Keys[i]);
                        sParams.Append("=");
                        sParams.Append(string.Join(",", _params.GetValues(_params.Keys[i])));
                    }
                }
            }
            LogHelper.LogEi(LogLevel.Info,message.ToString(),controllerName,actionName,sParams.ToString(),userName, url,IP);
            base.OnActionExecuted(filterContext);
        }
    }
}