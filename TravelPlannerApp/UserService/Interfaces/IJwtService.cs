using UserService.Models;

namespace UserService.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}