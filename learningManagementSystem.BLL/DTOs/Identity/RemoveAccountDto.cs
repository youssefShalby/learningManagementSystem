
namespace learningManagementSystem.BLL.DTOs;

public class RemoveAccountDto
{
	[JsonIgnore]
	public string Email { get; set; } = string.Empty;

	[Required]
	public string Password { get; set; } = string.Empty;
}
