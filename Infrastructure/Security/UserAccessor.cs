using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Infrastructure.Security
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpcontextaccessor;

        public UserAccessor(IHttpContextAccessor httpcontextaccessor)
        {
            _httpcontextaccessor = httpcontextaccessor ?? throw new System.ArgumentNullException(nameof(httpcontextaccessor));
        }

        public string GetCurrentUserName()
        {
            var username = _httpcontextaccessor.HttpContext.User?.Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            return username;
        }
    }
}