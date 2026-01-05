using ECommercePortal.API.GraphQL.Queries;
using ECommercePortal.Domain.Entities;
using ECommercePortal.Infrastructure.Persistence;
using HotChocolate.Authorization;

namespace ECommercePortal.API.GraphQL.Product
{
    [ExtendObjectType(typeof(Query))]
    public class ProductQueries
    {
        [AllowAnonymous]
        public IQueryable<ProductDetail> GetAllProducts(
        [Service] AppDbContext context)
        => context.Products.Where(p => p.IsActive);

        public IQueryable<ProductDetail> GetOwnersProducts(
            Guid ownerId,
        [Service] AppDbContext context)
        => context.Products.Where(p => p.IsActive && p.ProductOwnerId == ownerId);

        public ProductDetail? GetProductById(
            Guid productId,
            [Service] AppDbContext context)
            => context.Products.FirstOrDefault(p => p.ProductId == productId);
    }
}
