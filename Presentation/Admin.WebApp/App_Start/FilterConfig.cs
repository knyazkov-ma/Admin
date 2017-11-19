using Admin.WebApp.Filters;
using System.Web.Mvc;

namespace Admin.WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new RecaptchaFilter());
        }
    }
}
