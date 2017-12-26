using NLog;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace FunThing.Class
{
    /// <summary>
    /// 自定义错误处理机制
    /// </summary>
    public class CustomExceptionAttribute: HandleErrorAttribute
    {
       
        public override void OnException(ExceptionContext filterContext)
        {
            string controllerNamer = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();
            string userName = filterContext.HttpContext.User.Identity.Name;
            string url = filterContext.HttpContext.Request.Url == null ? string.Empty : HttpUtility.UrlDecode(filterContext.HttpContext.Request.Url.AbsoluteUri, Encoding.UTF8);
            string IP = HttpContext.Current.Request.Cookies.AllKeys.Contains("KOAL_CLIENT_IP") ? HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies["KOAL_CLIENT_IP"].Value):WebHelper.GetRealIP();
            var _params = filterContext.HttpContext.Request.Form;
            StringBuilder sParams = new StringBuilder();
            if (_params.Count != 0)
            {
                for (int i = 0; i < _params.Count; i++)
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
            LogHelper.LogEi(LogLevel.Error,filterContext.Exception.ToString(),controllerNamer,actionName,sParams.ToString(),userName,url,IP);
            base.OnException(filterContext);

        }
    }
}