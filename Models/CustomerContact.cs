namespace myfreelas.Models;

public class CustomerContact
{
    public int Id { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Other { get; set; }
    
    //Navigation property 
    public Customer Customer { get; set; }
    public int CustomerId { get; set; }
}
