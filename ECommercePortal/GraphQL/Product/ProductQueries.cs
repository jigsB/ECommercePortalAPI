using ECommercePortal.Infrastructure.Persistence;
using HotChocolate.Authorization;

namespace ECommercePortal.API.GraphQL.Product
{
    public class ProductQueries
    {
        [AllowAnonymous]
        public IQueryable<Domain.Entities.ProductDetail> GetProducts(
        [Service] AppDbContext context)
        {
            return context.Products.Where(p => p.IsActive);
        }

        public Domain.Entities.ProductDetail? GetProductById(
            Guid productId,
            [Service] AppDbContext context)
        {
            return context.Products.FirstOrDefault(p => p.ProductId == productId);
        }
    }
}
