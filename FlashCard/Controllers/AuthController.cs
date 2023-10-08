using FlashCard.Model.DTO;
using FlashCard.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlashCard.Controllers;

public class AuthController : BaseApiController
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly IConfiguration _config;

	public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_config = config;
	}

	[HttpPost]
	[Route("seed-roles")]
	public async Task<IActionResult> SeedRoles()
	{
		bool isAdminRoleExist = await _roleManager.RoleExistsAsync(StaticUserRoles.ADMIN);
		bool isUseRoleExist = await _roleManager.RoleExistsAsync(StaticUserRoles.USER);

		if (isAdminRoleExist && isUseRoleExist)
		{
			return Ok("Roles seeding is already done");
		}

		await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.USER));
		await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.ADMIN));

		return Ok("Role seeding done Succesfuly");
	}

	[HttpPost]
	[Route("register")]
	public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
	{
		var isExists = await _userManager.FindByNameAsync(registerDto.UserName);

		if (isExists != null)
		{
			return BadRequest("Username is already exists");
		}

		var newUser = new ApplicationUser()
		{
			FirstName = registerDto.FirstName,
			LastName = registerDto.LastName,
			Email = registerDto.Email,
			UserName = registerDto.UserName,
			SecurityStamp = Guid.NewGuid().ToString()
		};

		var createUserResult = await _userManager.CreateAsync(newUser, registerDto.Password);

		if (!createUserResult.Succeeded)
		{
			var errorString = "UserCreation Failed";
			foreach (var error in createUserResult.Errors)
			{
				errorString += " #" + error.Description;
			}
			return BadRequest(errorString);
		}

		await _userManager.AddToRoleAsync(newUser, StaticUserRoles.USER);

		return Ok("User was created succesfully");
	}

	[HttpPost]
	[Route("login")]
	public async Task<IActionResult> Login([FromBody] Login loginDto)
	{
		var user = await _userManager.FindByNameAsync(loginDto.UserName);

		if (user is null)
		{
			return Unauthorized("Invalid credentials");
		}

		var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);

		if (!isPasswordCorrect)
		{
			return Unauthorized("Invalid password");
		}

		var userRoles = await _userManager.GetRolesAsync(user);

		var authClaims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, user.UserName),
			new Claim(ClaimTypes.NameIdentifier, user.Id),
			new Claim("JWTID", Guid.NewGuid().ToString()),
			new Claim("FirstName", user.FirstName),
			new Claim("LastName", user.LastName)
		};
		foreach (var userRole in userRoles)
		{
			authClaims.Add(new Claim(ClaimTypes.Role, userRole));
		}

		var token = GenerateNewJsonWebToken(authClaims);

		return Ok(token);
	}

	private string GenerateNewJsonWebToken(List<Claim> claims)
	{
		var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));

		var tokenObj = new JwtSecurityToken(
			issuer: _config["Jwt:Issuer"],
			audience: _config["Jwt:Audience"],
			expires: DateTime.Now.AddDays(1),
			claims: claims,
			signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
			);
		string token = new JwtSecurityTokenHandler().WriteToken(tokenObj);

		return token;
	}

	//make user => admin
	[HttpPost]
	[Route("make-admin")]
	public async Task<IActionResult> MakeAdmin([FromBody] UpdatePermissionDto updatePermissionDto)
	{
		var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

		if (user is null)
		{
			return BadRequest("Invalid UserName");

		}

		await _userManager.AddToRoleAsync(user, StaticUserRoles.ADMIN);

		return Ok("User is now an admin");

	}
}