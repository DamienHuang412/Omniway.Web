using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Practice.Web.Api.Interfaces;

namespace Practice.Web.Api.Attributes;

public class AllowlistAuthorizeAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var agent = context.HttpContext.Request.Headers.UserAgent.ToString();
        if (context.HttpContext.User.Identity is not { IsAuthenticated: true })
        {
            context.Result = new UnauthorizedResult();
        }

        var requestUser = context.HttpContext.User.Identity.Name;

        if (string.IsNullOrEmpty(requestUser))
        {
            context.Result = new UnauthorizedResult();
        }
        
        var allowlistManager = context.HttpContext.RequestServices.GetRequiredService<IAllowlistManager>();

        if (!allowlistManager.Authorize(requestUser))
        {
            context.Result = new UnauthorizedResult();
        }
        
        base.OnActionExecuting(context);
    }
}