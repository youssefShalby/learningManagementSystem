
namespace E_Commerce.BLL.DTOs;

public class VerificationCodeDto
{
	public string UserId { get; set; } = string.Empty;

    [JsonIgnore]
	public string Code { get; set; } = string.Empty;
    public VerificationCodeDto(string userId, string code = "")
    {
        UserId = userId;
        Code = code;
    }
}
