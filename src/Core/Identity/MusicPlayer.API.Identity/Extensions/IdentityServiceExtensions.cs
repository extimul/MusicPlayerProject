namespace MusicPlayer.API.Identity.Extensions;

public static class IdentityServiceExtensions
{
    public static IConfigurationBuilder AddClients(this IConfigurationBuilder builder, 
        string fileName = "identityclients.json") => builder.AddJsonFile(fileName, true);
}