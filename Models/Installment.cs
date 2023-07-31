using System.ComponentModel.DataAnnotations;

namespace myfreelas.Models;

public class Installment
{
    public int Id { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime Month { get; set; }
    public decimal Value { get; set; }

    //Navigation property 
    public Freela Freela { get; set; }
    public int FreelaId { get; set; }
}
