using HotChocolate.Authorization;
using System.Security.Claims;

namespace ECommercePortal.API.GraphQL.Queries
{

    [ExtendObjectType(typeof(Query))]
    public class UserQuery
    {
        
        public string Me(ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email);
        }
    }
}
