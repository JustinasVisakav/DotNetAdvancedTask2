using Microsoft.AspNetCore.Http;
using CatalogService.Configuration.Interfaces;
using Microsoft.AspNetCore.Routing;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace CatalogService.Infrastructure.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IConfig config)
        {
            if (context.Request.Method.Any() && config.EntitlementEnabled && !context.Request.Path.ToString().Contains("swagger"))
            {
                string authHeader = context.Request.Headers["Authorization"];
                if (!string.IsNullOrEmpty(authHeader))
                {
                    bool hasPermission = false;

                    switch (context.Request.Method.ToLower())
                    {
                        case "get":
                            hasPermission = CanRead(authHeader);
                            break;
                        case "post":
                            hasPermission = CanCreate(authHeader);
                            break;
                        case "delete":
                            hasPermission = CanDelete(authHeader);
                            break;
                        case "put":
                            hasPermission = CanUpdate(authHeader);
                            break;
                        default:
                            hasPermission = false;
                            break;
                    }

                    if(!hasPermission)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        await context.Response.WriteAsync("Unauthorized");

                        return;
                    }
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await context.Response.WriteAsync("Unauthorized");

                    return;
                }
            }

            await _next(context);
        }

        private bool CanRead(string token)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var permissions = jwtToken.Claims.FirstOrDefault(x => x.Type == "realm_access").Value.ToString().ToLower();
            if (permissions.Contains("read"))
            {
                return true;
            }

            return false;
        }

        private bool CanCreate(string token)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var permissions = jwtToken.Claims.FirstOrDefault(x => x.Type == "realm_access").Value.ToString().ToLower();
            if (permissions.Contains("create"))
            {
                return true;
            }

            return false;
        }

        private bool CanUpdate(string token)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var permissions = jwtToken.Claims.FirstOrDefault(x => x.Type == "realm_access").Value.ToString().ToLower();
            if (permissions.Contains("update"))
            {
                return true;
            }

            return false;
        }

        private bool CanDelete(string token)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var permissions = jwtToken.Claims.FirstOrDefault(x => x.Type == "realm_access").Value.ToString().ToLower();
            if (permissions.Contains("delete"))
            {
                return true;
            }

            return false;
        }
    }
}
