using System.ComponentModel.DataAnnotations;

namespace myfreelas.Dtos.Dashboard;

public class RequestGetMonthlyBillingJson
{
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
}
