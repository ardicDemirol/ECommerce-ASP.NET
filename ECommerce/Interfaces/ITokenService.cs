using ECommerce.Models;

namespace ECommerce.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
