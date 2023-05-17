using Graduation.WebUI.Infrastructure.Cache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Graduation.WebUI.Management.Authorize
{
    public class ClaimRequirementFilter
    {
        CacheHelper cacheHelper;

        public ClaimRequirementFilter(CacheHelper cacheHelper)
        {
            this.cacheHelper = cacheHelper;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var role = "";
                if(role == null)
            {
                context.Result = new RedirectResult("/Home/Login");
                return;
            }
            if (role == "1")
                return;
        }

    }
}
