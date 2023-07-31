using System.ComponentModel.DataAnnotations;

namespace myfreelas.Dtos.Freela;

public class ResponseInstallmentJson
{
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy")]
    public DateTime Month { get; set; }
    public decimal Value { get; set; }
}
