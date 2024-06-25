

namespace learningManagementSystem.BLL.DTOs;

public class RegisterDto
{
    public string DisplayName { get; set; } = string.Empty;

    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = string.Empty;
    public bool IsInstructor { get; set; } = false;
}
