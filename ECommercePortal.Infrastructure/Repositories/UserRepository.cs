using ECommercePortal.Domain.Entities;
using ECommercePortal.Domain.Response;
using ECommercePortal.Infrastructure.Persistence;
using ECommercePortal.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommercePortal.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var user = await _context.Users
                 .Where(u => u.Email == email)
                 .Select(u => new
                 {
                     u.UserId,
                     u.FullName,
                     u.Email,
                     RoleName = u.Role.RoleName
                 })
     .FirstOrDefaultAsync();
            return user is null ? null : new User
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Role = user?.RoleName is not null ? new Role { RoleName = user.RoleName } : null,
            };
        }



        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
