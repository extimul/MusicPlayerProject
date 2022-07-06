namespace ServerApp.WebApp.Base.Exceptions;

public class BaseServerException : Exception
{
    public string Title { get; }
    
    protected BaseServerException(string title, string message)
        : base(message) =>
        Title = title;
}