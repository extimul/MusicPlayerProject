using ServerApp.Base.Exceptions;

namespace ServerApp.Base.Extensions;

public static class CollectionExtensions
{
    public static T GetConfigureParam<T>(this object[]? objects)
    {
        var service = objects.FirstOrDefault(x => x.GetType() == typeof(T));
        if (service is null) throw new ServerException("Please check service configuration");
        return (T)service;
    }
}