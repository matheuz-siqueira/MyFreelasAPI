using myfreelas.Dtos.Freela;
using myfreelas.Models.Enums;

namespace myfreelas.Dtos.Customer;

public class ResponseCustomerJson
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Type { get; set; }
    public List<ResponseFreelasByCustomerJson> Freelas { get; set; }
}
