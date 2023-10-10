using FlashCard.Interfacces;
using FlashCard.Model.DTO.AuthDto;
using Microsoft.AspNetCore.Mvc;

namespace FlashCard.Controllers;

public class AuthController : BaseApiController
{
	private readonly IAuthService _authService;

	public AuthController(IAuthService authService)
	{
		_authService = authService;
	}

	[HttpPost]
	[Route("seed-roles")]
	public async Task<IActionResult> SeedRoles()
	{
		var seedRoles = await _authService.SeedRolesAsync();
		return Ok(seedRoles);
	}

	[HttpPost]
	[Route("register")]
	public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
	{
		var register = await _authService.RegisterAsync(registerDto);

		if (register.IsSuceed)
			return Ok(register);
		
		return BadRequest(register);
	}

	[HttpPost]
	[Route("login")]
	public async Task<IActionResult> Login([FromBody] Login loginDto)
	{
		var loginResult = await _authService.LoginAsync(loginDto);

		if (loginResult.IsSuceed)
			return Ok(loginResult);

		return Unauthorized(loginResult);

	}

	//make user => admin
	[HttpPost]
	[Route("make-admin")]
	public async Task<IActionResult> MakeAdminAsync([FromBody] UpdatePermissionDto updatePermissionDto)
	{
		var operationResult = await _authService.MakeAdminAsync(updatePermissionDto);

		if(operationResult.IsSuceed)
			return Ok(operationResult);

		return BadRequest(operationResult);
	}
}