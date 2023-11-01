namespace MyFreelas.Application.Exceptions.BaseException;

public class UserAlreadyExistsException : MyFreelasExceptions
{
    public UserAlreadyExistsException(string message) : base (message) {}
}
