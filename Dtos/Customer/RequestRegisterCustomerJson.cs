using myfreelas.Models;
using myfreelas.Models.Enums;

namespace myfreelas.Dtos.Customer;

public class RequestRegisterCustomerJson
{
    public string Name { get; set; }
    public CustomerEnum Type { get; set; }
    public string Email { get; set; }  
    public string PhoneNumber { get; set; }
}
