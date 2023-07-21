namespace myfreelas.Exceptions.BaseException;

public class CustomerAlreadyExistsException : MyFreelasExceptions
{
    public CustomerAlreadyExistsException(string message) : base (message) {}
}
