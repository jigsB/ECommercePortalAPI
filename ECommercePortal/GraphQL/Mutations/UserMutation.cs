using ECommercePortal.Application.DTOs;
using ECommercePortal.Application.Services;
using ECommercePortal.Domain.Entities;
using HotChocolate.Authorization;

namespace ECommercePortal.API.GraphQL.Mutations
{
    public class UserMutation
    {
        [Authorize(Roles = new[] { "Admin" })]
        public async Task<User> CreateUser(
        CreateUserInput input,
        [Service] UserService service)
        => await service.CreateUserAsync(input);
    }
}
