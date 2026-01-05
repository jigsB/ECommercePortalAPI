using ECommercePortal.API.GraphQL.Product.Inputs;
using ECommercePortal.Infrastructure.Persistence;
using HotChocolate.Authorization;
using System.Security.Claims;
using ECommercePortal.Domain.Entities;
using ECommercePortal.API.GraphQL.Queries;
using ECommercePortal.API.GraphQL.Mutations;
namespace ECommercePortal.API.GraphQL.Product
{
    [ExtendObjectType(typeof(Mutation))]
    public class ProductMutations
    {
        //[Authorize(Roles = new[] { "StoreOwner" })]
        public async Task<ProductDetail> AddProduct(
        CreateProductInput input,
        [Service] AppDbContext context)
        {
            var product = new ProductDetail
            {
                ProductId = Guid.NewGuid(),
                ProductOwnerId = input.ProductOwnerId,
                ProductName = input.ProductName,
                Description = input.Description,
                Price = input.Price,
                StockQuantity = input.StockQuantity
            };

            context.Products.Add(product);
            await context.SaveChangesAsync();
            return product;
        }

        [Authorize(Roles = new[] { "StoreOwner" })]
        public async Task<ProductDetail> UpdateProduct(
            Guid productId,
            UpdateProductInput input,
            [Service] AppDbContext context)
        {
            var product = await context.Products.FindAsync(productId)
                ?? throw new GraphQLException("Product not found");

            product.ProductName = input.ProductName;
            product.Description = input.Description;
            product.Price = input.Price;
            product.StockQuantity = input.StockQuantity;
            product.IsActive = input.IsActive;

            await context.SaveChangesAsync();
            return product;
        }

        [Authorize(Roles = new[] { "StoreOwner" })]
        public async Task<bool> DeleteProduct(
            Guid productId,
            [Service] AppDbContext context)
        {
            var product = await context.Products.FindAsync(productId)
                ?? throw new GraphQLException("Product not found");

            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
