using ECommercePortal.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommercePortal.Application.DTOs
{
    public record CreateUserInput(
      string Email,
      string Password,
      UserRole Role
    );

}
