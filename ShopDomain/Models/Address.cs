using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDomain.Models;

[Table("addresses")]
public class Address
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    [Column("apartment_number")]
    public int? ApartmentNumber { get; set; }

    [Column("house_number")]
    public string HouseNumber { get; set; } = string.Empty;

    [Required]
    [Column("street")]
    public string Street { get; set; } = string.Empty;

    [Required]
    [Column("city")]
    public string City { get; set; } = string.Empty;

    [Required]
    [Column("country")]
    public string Country { get; set; } = string.Empty;

    [Column("postal_code")]
    public string PostalCode { get; set; } = string.Empty;

}
