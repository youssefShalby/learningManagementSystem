

namespace learningManagementSystem.BLL.Service;

public interface IUserAccountManagmentService
{
	Task<CommonResponse> UpdatePasswordAsync(UpdatePasswordDto model);
	Task<CommonResponse> UpdateAccountInfoAsync(UpdateAccountInfoDto model, string email);
	Task<CommonResponse> LogoutAsync(string email);
	Task<CommonResponse> RemoveAccountAsync(RemoveAccountDto model);
}
