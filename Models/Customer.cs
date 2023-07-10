using myfreelas.Models.Enums;

namespace myfreelas.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CustomerEnum Type { get; set; }

    public List<CustomerContact> Contacts { get; set; }

    //Navigation property
    public User User { get; set; }
    public int UserId { get; set; }
}
