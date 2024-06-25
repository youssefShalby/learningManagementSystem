

namespace learningManagementSystem.API.Controllers;



[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly IUserService _userService;

	public AuthController(IUserService userService)
    {
		_userService = userService;
	}

	[HttpPost("Register")]
	public async Task<ActionResult> Register(RegisterDto model)
	{
		var result = await _userService.RegisterAsync(model);
		if (result.IsSuccessed)
		{
			return StatusCode(201, result.AdditionalInfo);
		}

		return BadRequest(result);
	}

	[HttpPost("ConfirmAccount")]
	public async Task<ActionResult> ConfirmAccount(ConfirmEmailDto model)
	{
		var result = await _userService.ConfirmEmailAsync(model);
		if (result.IsSuccessed)
		{
			return StatusCode(200, result);
		}

		return BadRequest(result);
	}

	[HttpPost("Login")]
	public async Task<ActionResult> Login(LoginDto model)
	{
		var result = await _userService.LoginAsync(model);
		if (result.IsSuccessed)
		{
			return StatusCode(200, result.AdditionalInfo);
		}

		return BadRequest(result);
	}

	[HttpPost("ForgetPassword")]
	public async Task<ActionResult> ForgetPassword([FromHeader]string email)
	{
		var result = await _userService.ForgetPasswordAsync(email);
		if (result.IsSuccessed)
		{
			return StatusCode(200, result);
		}

		return BadRequest(result);
	}

	[HttpPost("ResetPassword")]
	public async Task<ActionResult> ResetPassword(ResetPasswordDto model, [FromHeader]string token)
	{
		var result = await _userService.ResetPasswordAsync(model, token);
		if (result.IsSuccessed)
		{
			return StatusCode(200, result);
		}

		return BadRequest(result);
	}


	[HttpPost("ResendConfirmationEmail/{email}")]
	public async Task<ActionResult> ResendConfirmationEmail(string email)
	{
		var result = await _userService.ResendConfirmationEmail(email);
		if (!result.IsSuccessed)
		{
			return BadRequest(result); //> 400
		}
		return Ok(result); //> 200
	}
}
