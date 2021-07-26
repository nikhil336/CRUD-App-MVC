using System.Web;
using System.Web.Mvc;
using CRUD_App_Project.Models;

namespace CRUD_App_Project
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomExceptionFilter());
        }
    }
}
