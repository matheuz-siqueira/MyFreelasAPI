using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFreelas.Domain.Entities;

public sealed class User : BaseEntity
{

    [Required]
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; }

    [Required]
    [Column(TypeName = "varchar(100)")]
    public string LastName { get; set; }

    [Required]
    [Column(TypeName = "varchar(150)")]
    public string Email { get; set; }

    [Required]
    [Column(TypeName = "varchar(60)")]
    public string Password { get; set; }

    public List<Customer> Customers { get; set; }   
}

