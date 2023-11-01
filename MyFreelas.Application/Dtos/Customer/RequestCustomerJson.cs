using MyFreelas.Domain.Entities.Enums;

namespace MyFreelas.Application.Dtos.Customer;

public class RequestCustomerJson
{
    public string Name { get; set; }
    public CustomerEnum Type { get; set; }
    public string Email { get; set; }  
    public string PhoneNumber { get; set; }
    public string OtherContact { get; set; }
}
