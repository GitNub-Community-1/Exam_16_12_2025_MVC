using Domain.Dtos;

namespace Infastructure.Services.Interfaces;

public interface IAccountService
{
    Task<Response<LoginResponseDto>> LoginAsync(LoginDto loginDto);
    Task<Response<RegisterDto>> RegisterAsync(RegisterDto model);
}