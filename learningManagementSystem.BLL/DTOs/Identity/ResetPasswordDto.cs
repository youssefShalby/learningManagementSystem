namespace learningManagementSystem.BLL.DTOs;

public class ResetPasswordDto
{

	[EmailAddress(ErrorMessage = "This is not valid email address")]
	[Required(ErrorMessage = "Enter email address is required..!")]
	public string Email { get; set; } = string.Empty;

	public string NewPassword { get; set; } = string.Empty; //> validation already in service registeration

	[Required, Compare(nameof(NewPassword))]
	public string ConfirmPassword { get; set; } = string.Empty;
}