namespace ElipseshopMVC.Web.Infrastructure
{
    using System.Reflection;
    using System.Web.Mvc;

    public class AjaxRequestAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}