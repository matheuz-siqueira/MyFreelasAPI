using myfreelas.Models.Enums;

namespace myfreelas.Dtos;

public class RequestCustomerJson
{
    public string Name { get; set; }
    public CustomerEnum Type { get; set; }
    public string Email { get; set; }  
    public string PhoneNumber { get; set; }
    public string OtherContact { get; set; }
}
