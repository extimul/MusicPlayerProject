namespace ServerApp.WebApp.Base.Exceptions;

public class BadRequestException : BaseServerException
{
    public BadRequestException(string message) : base("Bad Request", message)
    {
    }
}