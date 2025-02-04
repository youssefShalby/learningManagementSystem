﻿
namespace learningManagementSystem.BLL.DTOs;

public class LoginDto
{
	[EmailAddress(ErrorMessage = "Email is not valid..!!")]
	[Required(ErrorMessage = "the email is required...!!")]
	public string Email { get; set; } = string.Empty;

	[Required]
	public string Password { get; set; } = string.Empty;
}
