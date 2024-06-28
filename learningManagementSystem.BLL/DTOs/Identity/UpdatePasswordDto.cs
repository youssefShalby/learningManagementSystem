
namespace learningManagementSystem.BLL.DTOs;

public class UpdatePasswordDto
{
    [JsonIgnore]
    public string email { get; set; } = string.Empty;
    public string OldPassword { get; set; } = string.Empty; 
    public string NewPassword { get; set; } = string.Empty; //> validation already in service registeration
	public string ConfirmPassword { get; set; } = string.Empty;
}
