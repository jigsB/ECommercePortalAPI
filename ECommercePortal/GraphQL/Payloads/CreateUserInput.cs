namespace ECommercePortal.API.GraphQL.Payloads
{
    public record CreateUserInput(
    string FullName,
    string Email,
    string Password,
    int RoleId
);
}
