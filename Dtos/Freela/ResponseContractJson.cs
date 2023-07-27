namespace myfreelas.Dtos.Freela;

public class ResponseContractJson
{
    public string Id { get; set; }
    public decimal Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
    public DateTime StartPayment { get; set; }
    public int PaymentInstallment { get; set; }

}
