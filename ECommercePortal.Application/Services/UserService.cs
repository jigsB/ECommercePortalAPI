using ECommercePortal.Application.DTOs;
using ECommercePortal.Domain.Entities;
using ECommercePortal.Infrastructure.Repositories.Interfaces;
using ECommercePortal.Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommercePortal.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<User> CreateUserAsync(CreateUserInput input)
        {
            if (await _repo.GetByEmailAsync(input.Email) != null)
                throw new GraphQLException("User already exists");

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Email = input.Email,
                PasswordHash = PasswordHasher.Hash(input.Password),
               // Role = 1, // Assuming Role is an integer representing UserRole
            };

            await _repo.AddAsync(user);
            return user;
        }
    }
}
