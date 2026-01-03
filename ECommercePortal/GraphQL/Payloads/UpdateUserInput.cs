namespace ECommercePortal.API.GraphQL.Payloads
{
    public record UpdateUserInput(
    Guid UserId,
    string FullName,
    string Email,
    int RoleId
);
}
