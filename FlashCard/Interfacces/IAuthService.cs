using FlashCard.Model.DTO.AuthDto;
using Microsoft.AspNetCore.Mvc;

namespace FlashCard.Interfacces;

public interface IAuthService
{
	Task<AuthServiceRequestDto> SeedRolesAsync();
	Task<AuthServiceRequestDto> RegisterAsync([FromBody] RegisterDto registerDto);
	Task<AuthServiceRequestDto> LoginAsync([FromBody] Login loginDto);
	Task<AuthServiceRequestDto> MakeAdminAsync([FromBody] UpdatePermissionDto updatePermissionDto);
}
