using Shared.Common;
using Shared.DTOs.User;

namespace UserService.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResult<AuthResponseDto>> Register(RegisterRequestDto request);
        Task<ServiceResult<AuthResponseDto>> Login(LoginRequestDto request);
    }
}