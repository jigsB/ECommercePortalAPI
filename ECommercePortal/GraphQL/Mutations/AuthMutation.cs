using ECommercePortal.Application.DTOs;
using ECommercePortal.Infrastructure.Repositories.Interfaces;
using ECommercePortal.Infrastructure.Security;

namespace ECommercePortal.API.GraphQL.Mutations
{
    public class AuthMutation
    {
        public async Task<string> Login(
        LoginInput input,
        [Service] IUserRepository repo,
        [Service] JwtService jwt)
        {
            var user = await repo.GetByEmailAsync(input.Email)
                ?? throw new GraphQLException("Invalid credentials");

            if (!PasswordHasher.Verify(input.Password, user.PasswordHash))
                throw new GraphQLException("Invalid credentials");

            return jwt.GenerateToken(user);
        }
    }
}
