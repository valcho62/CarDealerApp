using System;
using System.IO;
using System.Web.Mvc;

namespace CarDealerApp.App_Data.Filters
{
    public class TimerAttribute :ActionFilterAttribute
    {
        public DateTime timeExecuteStart;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.timeExecuteStart = DateTime.Now;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var workingTime = DateTime.Now - this.timeExecuteStart;
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;

            var result = string.Format($"{this.timeExecuteStart} - {controllerName}.{actionName} - {workingTime}");
            result += Environment.NewLine;

            File.AppendAllText("E:\\action-times.txt",result);
        }
    }
}