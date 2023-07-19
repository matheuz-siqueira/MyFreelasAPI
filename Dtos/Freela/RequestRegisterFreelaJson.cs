using HashidsNet;

namespace myfreelas.Dtos.Freela;

public class RequestRegisterFreelaJson
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Value { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
    public string CustomerId { get; set; }
}
