namespace movieStorage.Registration.Exceptions;

public class ModelInvalidException : Exception
{
    private readonly string _modelState;
    public ModelInvalidException(string ModelState)
    {
        _modelState = ModelState;
    }
    public override string Message => $"Model state is invalid {_modelState}";
}