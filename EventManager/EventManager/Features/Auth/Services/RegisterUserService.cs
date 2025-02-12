using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using EventManager.Features.Users;

namespace EventManager.Features.Auth.Services
{
    public class RegisterUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public RegisterUserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterUserAsync(AuthModels.RegisterRequest request)
        {
            //Provera da je role validna
            if (request.Role != UserRole.Organizer && request.Role != UserRole.Participant)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Invalid role selection" });
            }

            var user = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return result;
            }

            await _userManager.AddToRoleAsync(user, request.Role.ToString());

            return IdentityResult.Success;
        }
    }
}
