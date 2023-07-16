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

    [Required]
    [Column(TypeName = "text")]
    public string Description { get; set; }

    [Required]
    [Column(TypeName = "decimal(9,3)")]
    public decimal Value { get; set; }

    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime FinishDate { get; set; }


    //Navigation property 
    public User User { get; set; }
    
    [Required]
    public int UserId { get; set; }

    public Customer Customer { get; set; }
    
    [Required]
    public int CustomerId { get; set; }
}
