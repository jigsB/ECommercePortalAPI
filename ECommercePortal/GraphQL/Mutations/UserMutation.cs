using ECommercePortal.API.GraphQL.Queries;
using ECommercePortal.API.GraphQL.Types;
using ECommercePortal.Application.DTOs;
using ECommercePortal.Application.Services;
using ECommercePortal.Domain.Entities;
using ECommercePortal.Domain.Enums;
using ECommercePortal.Infrastructure.Security;
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
        public async Task<UserViewType> CreateUser(CreateUserInput input, [Service] JwtService jwt)
        {
            var user = await _userService.CreateUserAsync(input);

            var userData = new User
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Role = new Role { RoleName = ((UserRole)user.RoleId).ToString() } //: null((UserRole)user.RoleId).ToString();// Enum.GetName(typeof(UserRole), user.RoleId).ToString();
            };
            var token = jwt.GenerateToken(userData);



            //return user;
            return new UserViewType()
            {
                Token = token,
                User = new UserType
                {
                    UserId = user.UserId,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = userData?.Role.RoleName??"buyer"                }
            };
        }
    }
}
