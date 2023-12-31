using System.ComponentModel.DataAnnotations;

namespace MyFreelas.Application.Dtos.Freela;

public class ResponseFreelaJson
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy")]
    public DateTime StartDate { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy")]
    public DateTime FinishDate { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy")]
    public DateTime StartPayment { get; set; }
    public int PaymentInstallment { get; set; }
    public List<ResponseInstallmentJson> Installments { get; set; }
}
