using System.Security.Claims;

namespace FocusAPI.Services
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId { get; }
        Boolean IsAdmin { get; }
    }
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;
        public int? GetUserId => User is null ? null : (int?)int.Parse(User.FindFirst(c => c.Type == ClaimTypes.Name).Value);
        public Boolean IsAdmin => User is null ? false : User.FindFirst(c => c.Type == ClaimTypes.Role).Value == "Admin";
    }
}
