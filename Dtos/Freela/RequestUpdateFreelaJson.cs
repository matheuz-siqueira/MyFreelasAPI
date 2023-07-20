namespace myfreelas.Dtos.Freela;

public class RequestUpdateFreelaJson
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Value { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
}
