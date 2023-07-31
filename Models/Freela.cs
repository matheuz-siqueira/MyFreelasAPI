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

    [Required]
    [Column(TypeName = "decimal(9,3)")]
    public decimal Price { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime FinishDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime StartPayment { get; set; }
    public int PaymentInstallment { get; set; }


    //Navigation property 
    public User User { get; set; }

    [Required]
    public int UserId { get; set; }
    public Customer Customer { get; set; }
    [Required]
    public int CustomerId { get; set; }

    public List<Installment> Installments { get; set; } = new List<Installment>();


}

