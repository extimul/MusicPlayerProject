using IdentityServer4.Models;

namespace MusicPlayer.API.Identity.Persistence;

public static class IdentityConfig
{
    public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
    {
        new("musicplayer.webapi")
        {
            Scopes =
            {
                "musicplayer.webapi.fullaccess"
            }
        }
    };

    public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
    {
        new("musicplayer.webapi.fullaccess", "Full Access")
    };

    public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Email(),
        new IdentityResources.Phone(),
    };
}