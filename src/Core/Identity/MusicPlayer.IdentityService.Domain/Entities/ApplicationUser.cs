using Microsoft.AspNetCore.Identity;
using ServerApp.WebApp.Base.Entities;

namespace MusicPlayer.IdentityService.Domain.Entities;

public class ApplicationUser : IdentityUser<long>, IBaseEntity<long>
{
    public string Name { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}