using EMS.Model.IdentityModel;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EMS.Service.Users
{
    public class UserSessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserIdentity GetLoginUserInfo()
        {
            try
            {
                var identity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    UserIdentity userIdentity = new UserIdentity();
                    int userIdClaim = int.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);

                    if ( userIdClaim >0)
                    {
                        userIdentity.Id = userIdClaim;
                        userIdentity.Name = identity.FindFirst(ClaimTypes.Name).Value.ToString();
                        userIdentity.Email = identity.FindFirst(ClaimTypes.Email).Value.ToString();
                    }
                    return userIdentity;
                }
            }
            catch (Exception ex)
            {

            }
            return new UserIdentity();
        }
    }
}
