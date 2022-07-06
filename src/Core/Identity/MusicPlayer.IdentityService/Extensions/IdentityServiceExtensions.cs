namespace MusicPlayer.IdentityService.Extensions;

public static class IdentityServiceExtensions
{
    public static void AddClients(this IConfigurationBuilder builder,
        string fileName = "identityclients.json")
    {
        builder.AddJsonFile(fileName, true);
    }
}