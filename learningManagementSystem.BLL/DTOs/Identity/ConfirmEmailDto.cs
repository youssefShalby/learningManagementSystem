
namespace learningManagementSystem.BLL.DTOs;

public class ConfirmEmailDto
{
	public string UserId { get; set; } = string.Empty;
	public string Code { get; set; } = string.Empty;
    public ConfirmEmailDto(string userId, string code = "")
    {
        UserId = userId;
        Code = code;
    }
}
