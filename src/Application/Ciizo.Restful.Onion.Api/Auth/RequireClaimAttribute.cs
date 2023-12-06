using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ciizo.Restful.Onion.Api.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequireClaimAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _type;
        private readonly string _value;

        public RequireClaimAttribute(string type, string value)
        {
            _type = type;
            _value = value;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.HasClaim(_type, _value))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}