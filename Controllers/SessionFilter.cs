using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

public class SessionFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string? userId = context.HttpContext.Request.Cookies["userId"];

        if (string.IsNullOrEmpty(userId))
        {
            // Oturum kapalı, giriş yapma sayfasına yönlendirme yapabilir veya hata sayfası gösterebilirsiniz
            //context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "SignIn", action = "SignIn" }));
            if (context.ActionDescriptor.RouteValues["controller"] != "Home" && context.ActionDescriptor.RouteValues["action"] != "Index")
            {
                //context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
            }
        }
    }
}
