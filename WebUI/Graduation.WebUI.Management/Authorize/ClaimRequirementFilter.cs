using graduation.Data;
using Graduation.WebUI.Infrastructure.Cache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Graduation.WebUI.Management.Authorize
{
    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        CacheHelper cacheHelper;
        RolePageData _rolePageData;
        public ClaimRequirementFilter(CacheHelper cacheHelper, RolePageData _rolePageData)
        {
            this.cacheHelper = cacheHelper;
            this._rolePageData = _rolePageData;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
            {

            var role = context.HttpContext.User.Claims.FirstOrDefault(x =>x.Type == System.Security.Claims.ClaimTypes.Role);
                if(role == null || role.Value == "")
            {
                context.Result = new RedirectResult("/Home/Login");
                return;
            }
            if (role.Value == "1")
                return;

            var role_id = int.Parse(role.Value);
            var rolePages = cacheHelper.RolePages(role_id);

            //admin değilse kontrol edicemm
            var route = (context.RouteData.Values["controller"] + "/" + context.RouteData.Values["action"]).ToLowerInvariant();
            if(!rolePages.Any(x=>x.Route ==route))
            {
                var role_page = _rolePageData.GetBy(x => x.Route == route && x.RoleId == role_id).FirstOrDefault();

                if(role_page == null)
                {
                    if(context.HttpContext.Request.Headers.ContainsKey("x-requested-with"))
                    {
                
                        var xRequestedWithCheck = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "x-requested-with");
                        var value = xRequestedWithCheck.Value;
                        if(value == "XMLHttpRequest")
                        {
                            context.Result = new JsonResult("özel mesaj") { StatusCode = 403 };
                            return;

                        }
                    }
                    context.Result = new RedirectResult("/Home/_403?url=" + route);
                }
            }
        }

    }
}
