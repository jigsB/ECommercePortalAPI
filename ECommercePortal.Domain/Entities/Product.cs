using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommercePortal.Domain.Entities
{
    public class ProductDetail
    {
            [Key]
            public Guid ProductId { get; set; }

            [Required]
            [ForeignKey(nameof(ProductOwner))]
            public Guid ProductOwnerId { get; set; }

            public User ProductOwner { get; set; } = null!;

            [Required]
            [MaxLength(200)]
            public string ProductName { get; set; } = null!;

            public string? Description { get; set; }

            [Column(TypeName = "decimal(10,2)")]
            public decimal Price { get; set; }

            public int StockQuantity { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
