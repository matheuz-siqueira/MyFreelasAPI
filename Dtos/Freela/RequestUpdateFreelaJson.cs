namespace myfreelas.Dtos.Freela;

public class RequestUpdateFreelaJson
{
    public string Name { get; set; }
    public string Description { get; set; }
    public RequestRegisterContractJson Contract { get; set; }
}
