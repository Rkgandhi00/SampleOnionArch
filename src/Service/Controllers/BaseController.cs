using Common.CustomErrorResponses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace Service.Controllers
{
    public class BaseController : Controller
    {
        protected ApiResponse CustomResponse(HttpStatusCode statusCode, List<string>? message = null, object? result = null, IEnumerable<string>? errors = null)
        {
            var apiResponse = new ApiResponse();
            apiResponse.StatusCode = statusCode;
            apiResponse.Message = message;
            apiResponse.Result = result;
            apiResponse.Errors = errors;

            return apiResponse;
        }

        protected async Task<bool?> VerifyRoles(HttpContext context, List<string> validRoles)
        {
            bool isValid = false;
            var roles = await GetRoles(context);
            roles = roles.Select(x => x.ToLower()).ToList();
            if (roles == null || roles.Count == 0)
            {
                return null;
            }

            if (roles != null && roles?.Count > 0)
            {
                roles.ForEach(x => x.ToLower());
                foreach (var role in validRoles)
                {
                    isValid = roles.Contains(role.ToLower());
                    if (isValid)
                    {
                        break;
                    }
                }
            }

            return isValid;
        }

        protected async Task<List<string>> GetRoles(HttpContext context)
        {
            List<string> roles = new List<string>();
            var accessToken = await context.GetTokenAsync("access_token");
            if (accessToken != null)
            {
                var accessClaims = new JwtSecurityToken(accessToken).Claims;
                roles = new List<string>();
                foreach (Claim item in accessClaims)
                {
                    if (item.Type == "role")
                    {
                        roles.Add(item.Value);
                    }
                }
            }
            return roles;
        }
    }
}
