using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myfreelas.Models;

public class Freela
{
    [Required]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(120)")]
    public string Name { get; set; }

    [Column(TypeName = "text")]
    public string Description { get; set; }

    //Navigation property 
    public User User { get; set; }

    [Required]
    public int UserId { get; set; }

    public Customer Customer { get; set; }

    [Required]
    public int CustomerId { get; set; }
    public Contract Contract { get; set; }

}

