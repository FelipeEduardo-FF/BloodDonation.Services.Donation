using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Shared.Infra.HttpServices
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor.HttpContext;

            if (httpContext is not null && httpContext.User is not null && httpContext.User.Identity!.IsAuthenticated)
            {
                Name = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Id = httpContext.User.FindFirst(ClaimTypes.Sid)?.Value;


                if (httpContext.Request.Headers.TryGetValue("Authorization", out var token))
                {

                    if (token.ToString().StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                    {
                        Token = token.ToString().Substring("Bearer ".Length).Trim();
                    }
                }
            }
        }

        public string? Name { get; }
        public string? Id { get; }
        public string? Token { get; } // Propriedade para armazenar o token
    }

}
