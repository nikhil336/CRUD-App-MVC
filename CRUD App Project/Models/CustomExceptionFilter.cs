using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_App_Project.Models
{
    public class CustomExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext exceptionContext)
        {
            if (!exceptionContext.ExceptionHandled)
            {
                var exceptionMessage = exceptionContext.Exception.Message;
                var stackTrace = exceptionContext.Exception.StackTrace;
                var controllerName = exceptionContext.RouteData.Values["controller"].ToString();
                var actionName = exceptionContext.RouteData.Values["action"].ToString();
                var message = "Date: " + DateTime.Now.ToString() + " Controller: " + controllerName + " Action: " + actionName + " Error message: " + exceptionMessage + "\nStack Trace: " + stackTrace;

                File.AppendAllText(HttpContext.Current.Server.MapPath("~/Log/Log.txt"), message);

                Debug.WriteLine(message);
                exceptionContext.ExceptionHandled = true;
                exceptionContext.Result = new ViewResult()
                {
                    ViewName = "Error"
                };
            }
        }
    }
}