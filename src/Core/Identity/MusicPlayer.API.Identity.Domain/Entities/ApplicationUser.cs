using Microsoft.AspNetCore.Identity;
using MusicPlayer.API.Base.Entities;

namespace MusicPlayer.API.Identity.Domain.Entities;

public class ApplicationUser : IdentityUser<long>, IBaseEntity<long>
{
    public string? Name { get; set; }
    
    public string? Surname { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}