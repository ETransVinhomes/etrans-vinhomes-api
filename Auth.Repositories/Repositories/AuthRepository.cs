using Auth.Domains.Entities;
using Auth.Repositories.Data;
using Auth.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Auth.Repositories.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _dbContext;
        public AuthRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AppRole> CreateRoleAsync(AppRole role)
        {
            _dbContext.AppRole.Add(role);
            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return await _dbContext.AppRole.FirstAsync(x => x.Name == role.Name);
            }
            else throw new Exception("Save changes Failed!");
        }

        public async Task<AppUser> CreateUserAsync(AppUser user)
        {
            _dbContext.Users.Add(user);
            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return await _dbContext.AppUser.FirstAsync(x => x.Id == user.Id);
            }
            else throw new Exception("Save changes Failed!");
        }

        public async Task<bool> DeleteUserAsync(AppUser user)
        {
            _dbContext.AppUser.Remove(user);

            var result = await _dbContext.SaveChangesAsync() > 0 ? true : false;
            return result;
        }

        public Task<AppUser> FindUserAsync(Expression<Func<AppUser, bool>> expression) => _dbContext
            .AppUser.FirstAsync<AppUser>(expression);

        public Task<AppRole> GetRoleByNameAsync(string roleName) => _dbContext.Roles.FirstAsync(x => x.Name!.ToUpper() == roleName.ToUpper());



        public async Task<AppUser> GetUserByIdAsync(Guid id) => await _dbContext.AppUser.FirstAsync(x => x.Id == id);
        public async Task<AppUser> UpdateUserAsync(AppUser user)
        {
            _dbContext.AppUser.Update(user);
            return await _dbContext
                .SaveChangesAsync() > 0 ?
                await _dbContext.AppUser.FirstAsync(x => x.Id == user.Id) :
                throw new Exception("Save changes failed");
        }
    }
}
