using LeaveMangement_Application.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMangementAPI.Authorization
{
    /// <summary>
    /// 自定义过滤器：过滤权限
    /// </summary>
    public class SportFilter : Attribute,IActionFilter
    {
        private string Path { get; set; }
        private readonly ICommonAppService _commonAppService = new CommonAppService();
        public SportFilter(string path)
        {
            this.Path = path;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new System.NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            HttpContext httpContext = context.HttpContext;
            string account = GetAccountAsync(httpContext).Result;
            if (!_commonAppService.JudgeAuth(account, this.Path))
            {
                context.Result = new ContentResult()
                {
                    Content = "Resource unavailable - header should not be set",
                    StatusCode = 403
                };
            }
            //throw new System.NotImplementedException();
        }
        private async Task<string> GetAccountAsync(HttpContext httpContext)
        {
            string account = "";
            var schemeProvider = httpContext.RequestServices.GetService(typeof(IAuthenticationSchemeProvider)) as IAuthenticationSchemeProvider;
            var defaultAuthenticate = await schemeProvider.GetDefaultAuthenticateSchemeAsync();
            if (defaultAuthenticate != null)
            {
                var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                var user = result?.Principal;
                if (user != null)
                {
                    account = user.Identity.Name;
                }
            }
            return account;
        }
    }
}
