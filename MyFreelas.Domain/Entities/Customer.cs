using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyFreelas.Domain.Entities.Enums;

namespace MyFreelas.Domain.Entities;

public sealed class Customer : BaseEntity
{   
    [Required]
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; }

    [Required]
    public CustomerEnum Type { get; set; }
    
    [Required]
    [Column(TypeName = "varchar(150)")]
    public string Email { get; set; }

    [Required]
    [Column(TypeName = "varchar(20)")]
    public string PhoneNumber { get; set; }

    [Column(TypeName = "varchar(150)")]
    public string OtherContact { get; set; }

    public List<Freela> Freelas { get; set; }

    //Navigation property
    public User User { get; set; }
    public int UserId { get; set; }
}
