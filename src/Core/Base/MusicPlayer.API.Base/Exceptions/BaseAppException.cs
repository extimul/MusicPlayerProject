using System.Reflection;

namespace MusicPlayer.API.Base.Exceptions;

public class BaseAppException : Exception
{
    public BaseAppException()
    {
    }
    
    public BaseAppException(string? message) : 
        base($"An exception occurred \n{DateTime.Now} - {Assembly.GetCallingAssembly()}: {message}") {}
}