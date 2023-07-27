using System.Diagnostics.Contracts;
using HashidsNet;

namespace myfreelas.Dtos.Freela;

public class RequestRegisterFreelaJson
{
    public string Name { get; set; }
    public string Description { get; set; }
    public RequestRegisterContractJson Contract { get; set; }
    public string CustomerId { get; set; }
}

