using NLog;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FunThing.Class
{
    /// <summary>
    /// 记录当前页面的执行时间
    /// </summary>
    public class TrackPageLoadPerformanceAttribute : ActionFilterAttribute
    {
        //创建字典来记录开始时间，key是访问的线程id
        private readonly Dictionary<int, DateTime> _start = new Dictionary<int, DateTime>();
        //创建字典来记录当前访问的页面Url
        private readonly Dictionary<int, string> _url = new Dictionary<int, string>();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //过滤掉ChildAction,因为ChildAction实际上不是一个单独的页面
            if (filterContext.IsChildAction)
            {
                return;
            }
            var currentThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            try
            {
                _start.Add(currentThreadId, DateTime.Now);
                _url.Add(currentThreadId, filterContext.HttpContext.Request.Url == null ? string.Empty : HttpUtility.UrlDecode(filterContext.HttpContext.Request.Url.AbsoluteUri,Encoding.UTF8));
            }
            catch (Exception ex)
            {
                LogHelper.LogEi(LogLevel.Error,ex.ToString(), "TrackPageLoadPerformanceAttribute", "OnActionExecuting");
            }
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var currentThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            if (!_start.ContainsKey(currentThreadId))
            {
                return;
            }
            try
            {
                var costSeconds = (DateTime.Now - _start[currentThreadId]).TotalSeconds;
                //如果访问时间超出1秒，就打印出来。
                if (costSeconds > 1000)
                {
                    Log.Warn($"{_url[currentThreadId]}访问时间：{costSeconds}毫秒");
                    LogHelper.LogEi(LogLevel.Warn, $"{_url[currentThreadId]}访问时间：{costSeconds}毫秒",url: _url[currentThreadId]);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogEi(LogLevel.Error, ex.ToString(), "TrackPageLoadPerformanceAttribute", "OnResultExecuted");
            }
            finally
            {
                _start.Remove(currentThreadId);
                _url.Remove(currentThreadId);
            }
        }
    }
}