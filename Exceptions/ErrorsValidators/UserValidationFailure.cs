namespace myfreelas.Exceptions.ErrorsValidators;

public class UserValidationFailure
{
    public string PropertyName { get; set; }
    public string ErrorMessage { get; set; }    

    public UserValidationFailure(string propertyName, string errorMessage)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
    }
}
