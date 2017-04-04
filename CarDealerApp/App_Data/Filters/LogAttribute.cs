using System;
using System.IO;
using System.Web.Mvc;

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
           // var exeption = filterContext.Exception.Source;
            //var exeptMessage = filterContext.Exception.Message;
            var log = string.Format(
                $"{date} - {ipAddress} - {userName} - {controller}.{action}");
            //if (exeptMessage != null)
            //{
            //    log = "[!] " + log + " - " + "" + " - " + exeptMessage;
            //}
            log += Environment.NewLine;
            File.AppendAllText("e:\\logs.txt",log);
        }
    }
}