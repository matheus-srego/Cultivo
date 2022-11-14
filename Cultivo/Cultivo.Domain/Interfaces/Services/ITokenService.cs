using Cultivo.Domain.Models;

namespace Cultivo.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(Login login);
    }
}
