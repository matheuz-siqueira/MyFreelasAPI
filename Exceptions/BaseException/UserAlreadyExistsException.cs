namespace myfreelas.Exceptions.BaseException;

public class UserAlreadyExistsException : MyFreelasExceptions
{
    public UserAlreadyExistsException(string message) : base (message) {}
}
