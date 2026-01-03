using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommercePortal.Domain.Entities
{
    public class OrderItem
    {
        [Key]
        public Guid OrderItemId { get; set; }

        [Required]
        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }

        public Order Order { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }

        public ProductDetail Product { get; set; } = null!;

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }   // DB computed column
    }
}
