using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommercePortal.Domain.Entities
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        [Required]
        [ForeignKey(nameof(Buyer))]
        public Guid BuyerId { get; set; }

        public User Buyer { get; set; } = null!;

        public DateTime OrderDate { get; set; }

        [MaxLength(50)]
        public string OrderStatus { get; set; } = "Pending";

        [Column(TypeName = "decimal(12,2)")]
        public decimal TotalAmount { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}