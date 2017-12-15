using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FunThing.Controllers
{
    public class EchartsController : Controller
    {
        // GET: Echarts
        public ActionResult Index()
        {
            List<dynamic> Test = new List<dynamic>();
            
            int i = 0;
            while (i<30)
            {
                dynamic data = new ExpandoObject();
                data.num = i;
                data.ss = "测试";
                Test.Add(data);
                i++;
            }
            ViewBag.TestData = Test;
            return View();
        }
    }
}