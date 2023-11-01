using System.ComponentModel.DataAnnotations;

namespace MyFreelas.Application.Dtos.Freela;

public class RequestUpdateFreelaJson
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
}
