namespace myfreelas.Dtos.Freela;

public class RequestRegisterContractJson
{
    public decimal Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
    public DateTime StartPayment { get; set; }
    public int PaymentInstallment { get; set; }
}
