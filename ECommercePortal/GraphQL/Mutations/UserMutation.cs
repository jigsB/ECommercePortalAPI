using ECommercePortal.API.GraphQL.Queries;
using ECommercePortal.Application.DTOs;
using ECommercePortal.Application.Services;
using ECommercePortal.Domain.Entities;
using HotChocolate.Authorization;

namespace ECommercePortal.API.GraphQL.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class UserMutation
    {
        private readonly UserService _userService;

        public UserMutation(UserService userService)
        {
            _userService = userService;
        }
        //[Authorize(Roles = new[] { "Admin" })]
        public async Task<User> CreateUser(CreateUserInput input)
        {
            var user = await _userService.CreateUserAsync(input);
            return user;

        }
    }
}
