using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using MiniProjet.Domain.Models;

namespace YourProjectName.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly YourDbContext _dbContext;

        public RoleRepository(YourDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            return await _dbContext.Roles.FindAsync(roleId);
        }

    }

    public interface IRoleRepository
    {
    }
}
