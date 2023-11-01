namespace MyFreelas.Application.Exceptions.BaseException;

public class CustomerNotFoundException : MyFreelasExceptions
{
    public CustomerNotFoundException(string message) : base (message) {}
}
