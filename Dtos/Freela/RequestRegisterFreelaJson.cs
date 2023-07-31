using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using HashidsNet;

namespace myfreelas.Dtos.Freela;

public class RequestRegisterFreelaJson
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime FinishDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime StartPayment { get; set; }
    public int PaymentInstallment { get; set; }
    public string CustomerId { get; set; }
}

