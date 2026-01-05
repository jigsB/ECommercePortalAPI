using ECommercePortal.API.GraphQL.Types;
using ECommercePortal.Application.DTOs;
using ECommercePortal.Domain.Entities;
using ECommercePortal.Infrastructure.Repositories.Interfaces;
using ECommercePortal.Infrastructure.Security;

namespace ECommercePortal.API.GraphQL.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class AuthMutation
    {
        public async Task<UserViewType> Login(
        LoginInput input,
        [Service] IUserRepository repo,
        [Service] JwtService jwt)
        {
            var user = await repo.GetByEmailAsync(input.Email)
                ?? throw new GraphQLException("Invalid credentials");

            if (!PasswordHasher.Verify(input.Password, user.PasswordHash))
                throw new GraphQLException("Invalid credentials");

            var token = jwt.GenerateToken(user);

            //return new UserViewType()
            //{
            //    Token= token,
            //    User =
            //    {
            //          user.userId
            //    }
            //};
            return new UserViewType()
            {
                Token = token,
                User = new UserType
                {
                    UserId = user.UserId,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user?.Role.RoleName ?? "User"
                }
            };

        }
    }
}
