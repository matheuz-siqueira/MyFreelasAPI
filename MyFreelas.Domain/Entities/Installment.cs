using System.ComponentModel.DataAnnotations;

namespace MyFreelas.Domain.Entities;

public sealed class Installment : BaseEntity
{
    [DataType(DataType.Date)]
    public DateTime Month { get; set; }
    public decimal Value { get; set; }

    //Navigation property 
    public Freela Freela { get; set; }
    public int FreelaId { get; set; }
}
