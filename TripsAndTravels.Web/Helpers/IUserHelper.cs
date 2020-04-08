using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TripsAndTravels.Web.Data.Entities;
using TripsAndTravels.Web.Models;

namespace TripsAndTravels.Web.Helpers
{
    public interface IUserHelper
    {
        Task<UserEntity> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(UserEntity user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(UserEntity user, string roleName);

        Task<bool> IsUserInRoleAsync(UserEntity user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();
        Task<UserEntity> AddUserAsync(AddUserViewModel model, string path);


    }
}
