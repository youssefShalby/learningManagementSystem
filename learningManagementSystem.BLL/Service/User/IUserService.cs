

namespace learningManagementSystem.BLL.Service;

public interface IUserService
{
	Task<CommonResponse> RegisterAsync(RegisterDto model);
	Task<CommonResponse> ConfirmEmailAsync(ConfirmEmailDto model);
	Task<CommonResponse> LoginAsync(LoginDto model);
	Task<CommonResponse> ForgetPasswordAsync(string email);
	Task<CommonResponse> ResetPasswordAsync(ResetPasswordDto model, string token);
	Task<CommonResponse> LogoutAsync(string email);
	Task<CommonResponse> RemoveAccountAsync(RemoveAccountDto model);
	Task<CommonResponse> ResendConfirmationEmail(string email);
	Task<CommonResponse> UpdatePasswordAsync(UpdatePasswordDto model);
	Task<CommonResponse> UpdateAccountInfoAsync(UpdateAccountInfoDto model, string email);
	Task<ApplicationUser> GetUserByEmailAsync(string email);
}
