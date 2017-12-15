using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FunThing.Class
{
    /// <summary>
    /// 日志记录
    /// </summary>
    public class LogHelper
    {
        public static Logger log = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 通过LogEventInfo更新日志到数据库
        /// </summary>
        /// <param name="level">日志等级</param>
        /// <param name="message">日志详情</param>
        /// <param name="controller">控制器</param>
        /// <param name="action">动作</param>
        /// <param name="_params">参数</param>
        /// <param name="username">用户名</param>
        /// <param name="url">链接</param>
        public static void LogEi(LogLevel level, string message, string controller = "Default", string action = "Default", string _params = "Default", string username = "Default", string url = "Default", string IP = "Default")
        {
#if !DEBUG
            LogEventInfo ei = new LogEventInfo(level, "log", message);
            if(controller!= "Default")
            {
                ei.Properties["controller"] = controller;
            }
            if (action != "Default")
            {
                ei.Properties["action"] = action;
            }
            if (_params != "Default")
            {
                ei.Properties["params"] = _params;
            }
            if (username != "Default")
            {
                ei.Properties["username"] = username;
            }
            if(url!="Default")
            {
                ei.Properties["url"] = url;
            }
            if (IP != "Default")
            {
                ei.Properties["ip"] = IP;
            }
            log.Log(ei);
#endif
        }
    }
}