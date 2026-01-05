using ECommercePortal.Domain.Entities;
using ECommercePortal.Domain.Response;

namespace ECommercePortal.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
    }
}
