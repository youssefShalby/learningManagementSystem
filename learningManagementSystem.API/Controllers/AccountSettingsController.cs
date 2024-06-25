

namespace learningManagementSystem.API.Controllers;



[Route("api/[controller]")]
[ApiController]
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
		var result = await _userService.UpdatePasswordAsync(model);
		if (result.IsSuccessed)
		{
			return StatusCode(200, result);
		}

		return BadRequest(result);
	}

	[HttpPost("UpdateInfo")] //> TODO: take email auto from claims when add authorization
	public async Task<ActionResult> UpdateInfo(UpdateAccountInfoDto model, [FromHeader]string email)
	{
		var result = await _userService.UpdateAccountInfoAsync(model, email);
		if (result.IsSuccessed)
		{
			return StatusCode(200, result);
		}

		return BadRequest(result);
	}

	[HttpPost("Logout")]
	public async Task<ActionResult> Logout([FromHeader] string email)
	{
		var result = await _userService.LogoutAsync(email);
		if (result.IsSuccessed)
		{
			return StatusCode(200, result);
		}

		return BadRequest(result);
	}

	[HttpPost("RemoveAccount")]
	public async Task<ActionResult> RemoveAccount(RemoveAccountDto model)
	{
		var result = await _userService.RemoveAccountAsync(model);
		if (result.IsSuccessed)
		{
			return StatusCode(200, result);
		}

		return BadRequest(result);
	}
}
