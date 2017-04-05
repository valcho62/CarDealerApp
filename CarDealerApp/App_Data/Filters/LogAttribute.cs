using System;
using System.IO;
using System.Web.Mvc;
using CarDealerApp.Service;

namespace CarDealerApp.App_Data.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var date = DateTime.Now;
            var userName = filterContext.HttpContext.User.Identity.Name;
            var ipAddress = filterContext.HttpContext.Request.UserHostAddress;
            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var action = filterContext.ActionDescriptor.ActionName;
            var exeption = filterContext.Exception;

            if (userName == null)
            {
                userName = "Anonimus";
            }
            else
            {
                var session = filterContext.HttpContext.Request.Cookies.Get("sessionId");
                userName = AuthenticationManager.GetAuthenticatedUser(session.Value).Username;
            }
            var log = string.Format(
                $"{date} - {ipAddress} - {userName} - {controller}.{action}");
            if (exeption != null)
            {
                log = "[!] " + log + " - " + exeption.GetType().FullName + " - " + exeption.Message;
            }
            log += Environment.NewLine;
            File.AppendAllText("e:\\logs.txt",log);
        }
    }
}