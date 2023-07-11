using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using myfreelas.Models.Enums;

namespace myfreelas.Models;

public class Customer
{
    [Required]
    public int Id { get; set; }
    
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

    //Navigation property
    public User User { get; set; }
    public int UserId { get; set; }
}
