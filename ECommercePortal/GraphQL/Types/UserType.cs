namespace ECommercePortal.API.GraphQL.Types
{
    public class UserType
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

    }
    public class UserViewType
    {
        public string Token { get; set; }
        public UserType User { get; set; }
    }
}
