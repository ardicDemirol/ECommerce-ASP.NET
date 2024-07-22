using Microsoft.AspNetCore.Identity;

namespace ECommerce.Models;
public class AppUser : IdentityUser
{
    public List<Portfolio> Portfolios { get; set; } = new();

}
