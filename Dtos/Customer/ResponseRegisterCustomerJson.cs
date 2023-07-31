using myfreelas.Models.Enums;

namespace myfreelas.Dtos.Customer;

public class ResponseRegisterCustomerJson
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Type { get; set; }
}
