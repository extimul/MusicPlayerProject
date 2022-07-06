using System.Reflection;

namespace ServerApp.Base.Exceptions;

public class ServerException : Exception
{
    private const string Format = "An exception occurred \n{0} - {1}: {2}";
    
    public ServerException(string? message) : 
        base(string.Format(Format, DateTime.Now, Assembly.GetCallingAssembly(), message)) {}
    
    public ServerException(string? message, Exception? innerException) : 
        base(string.Format(Format, DateTime.Now, Assembly.GetCallingAssembly(), message), innerException) { }
}