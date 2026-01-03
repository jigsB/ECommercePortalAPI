using ECommercePortal.Domain.Entities;
using HotChocolate.Types;
namespace ECommercePortal.API.GraphQL.Product
{
    public class ProductType : ObjectType<ProductDetail>
    {
        protected override void Configure(IObjectTypeDescriptor<ProductDetail> descriptor)
        {
            descriptor.Ignore(x => x.ProductOwner);
            descriptor.Ignore(x => x.OrderItems);
        }
    }
}
