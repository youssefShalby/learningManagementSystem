

namespace learningManagementSystem.BLL.DTOs;

public class RegisterDto
{
    [MaxLength(80, ErrorMessage = "The Name cannot be over 80 char")]
    [MinLength(1, ErrorMessage = "Please enter at least one char")]
    public string DisplayName { get; set; } = string.Empty;

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

	[MaxLength(80, ErrorMessage = "The UserName cannot be over 80 char")]
	[MinLength(1, ErrorMessage = "Please UserName at least one char")]
	public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; //> validation in service registeration

    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = string.Empty;
    public bool IsInstructor { get; set; } = false;
}
