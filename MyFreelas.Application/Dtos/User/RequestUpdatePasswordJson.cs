namespace MyFreelas.Application.Dtos.User;

public class RequestUpdatePasswordJson
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}
