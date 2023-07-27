using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myfreelas.Models;

public class Contract
{
    [Required]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "decimal(9,3)")]
    public decimal Price { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime FinishDate { get; set; }

    [Required]
    public DateTime StartPayment { get; set; }
    public int PaymentInstallment { get; set; }

    // Navigation property
    public Freela Freela { get; set; }
    [Required]
    public int FreelaId { get; set; }

}
