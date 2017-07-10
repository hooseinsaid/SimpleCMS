using System.Web;
using System.Web.Mvc;

namespace CMS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new System.Web.Mvc.AuthorizeAttribute());
            //filters.Add(new RequireHttpsAttribute());

            /* 
            The Authorize filter prevents anonymous users from accessing any methods in the application. 
            You will use the AllowAnonymous attribute to opt out of the authorization requirement in a couple methods, so anonymous users can log in and can view the home page. 
            The RequireHttps requires that all access to the web app be through HTTPS. 
            */
        }
    }
}
