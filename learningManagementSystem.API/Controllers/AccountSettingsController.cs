

using Microsoft.AspNetCore.Authorization;

namespace learningManagementSystem.API.Controllers;



[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AccountSettingsController : ControllerBase
{
	private readonly IUserService _userService;

	public AccountSettingsController(IUserService userService)
	{
		_userService = userService;
	}

	[HttpPost("UpdatePassword")]
	public async Task<ActionResult> UpdatePassword(UpdatePasswordDto model)
	{
		var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
		model.email = userEmail ?? null!;
		var result = await _userService.UpdatePasswordAsync(model);
		if (result.IsSuccessed)
		{
			return StatusCode(200, result);
		}

		return BadRequest(result);
	}

	[HttpPost("UpdateInfo")] //> TODO: take email auto from claims when add authorization
	public async Task<ActionResult> UpdateInfo(UpdateAccountInfoDto model)
	{
		var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
		var result = await _userService.UpdateAccountInfoAsync(model, userEmail ?? null!);
		if (result.IsSuccessed)
		{
			return StatusCode(200, result);
		}

		return BadRequest(result);
	}

	[HttpPost("Logout")]
	public async Task<ActionResult> Logout()
	{
		var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
		var result = await _userService.LogoutAsync(userEmail ?? null!);
		if (result.IsSuccessed)
		{
			return StatusCode(200, result);
		}

		return BadRequest(result);
	}

	[HttpPost("RemoveAccount")]
	public async Task<ActionResult> RemoveAccount(RemoveAccountDto model)
	{
		var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
		model.Email = userEmail ?? null!;
		var result = await _userService.RemoveAccountAsync(model);
		if (result.IsSuccessed)
		{
			return StatusCode(200, result);
		}

		return BadRequest(result);
	}
}
