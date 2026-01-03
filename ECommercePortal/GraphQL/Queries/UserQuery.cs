using HotChocolate.Authorization;
using System.Security.Claims;

namespace ECommercePortal.API.GraphQL.Queries
{
    public class UserQuery
    {
        [Authorize]
        public string Me(ClaimsPrincipal user)
        => user.FindFirstValue(ClaimTypes.Email);
    }
}
