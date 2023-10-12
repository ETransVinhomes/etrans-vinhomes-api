using Auth.Domains.Entities;
using System.Linq.Expressions;

namespace Auth.Services.Repositories
{
    public interface IAuthRepository
    {
        Task<AppUser> GetUserByIdAsync(Guid id);
        Task<AppUser> FindUserAsync(Expression<Func<AppUser, bool>> expression);
        Task<AppUser> CreateUserAsync(AppUser user);
        Task<AppUser> UpdateUserAsync(AppUser user);
        Task<bool> DeleteUserAsync(AppUser user);
        Task<AppRole> GetRoleByNameAsync(string roleName);
        Task<AppRole> CreateRoleAsync(AppRole role);
        Task<IEnumerable<AppUser>> GetAllAsync();
    }
}
