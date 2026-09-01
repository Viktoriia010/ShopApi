using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDomain.Models;

[Table("order_details")]
public class OrderDetails
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [Column("order_id")]
    public Guid OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public Orders Order { get; set; } = null!;

    [Required]
    [Column("product_id")]
    public int ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]

    public Product Product { get; set; } = null!;

    [Required]
    [Column("price")]
    public decimal Price { get; set; }

    [Required]
    [Column("count")]
    public int Count { get; set; }

}
