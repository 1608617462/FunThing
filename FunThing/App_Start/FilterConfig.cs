using FunThing.Class;
using System.Web;
using System.Web.Mvc;

namespace FunThing
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new TrackPageLoadPerformanceAttribute());
            filters.Add(new CustomExceptionAttribute());
        }
    }
}
