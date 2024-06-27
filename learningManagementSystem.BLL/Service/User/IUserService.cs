

namespace learningManagementSystem.BLL.Service;

public interface IUserService
{
	Task<CommonResponse> RegisterAsync(RegisterDto model);
	Task<CommonResponse> RegisterAdminAsync(RegisterDto model);
	Task<CommonResponse> ConfirmEmailAsync(ConfirmEmailDto model);
	Task<CommonResponse> LoginAsync(LoginDto model);
	Task<CommonResponse> ForgetPasswordAsync(string email);
	Task<CommonResponse> ResetPasswordAsync(ResetPasswordDto model, string token);
	Task<CommonResponse> ResendConfirmationEmail(string email);
}
