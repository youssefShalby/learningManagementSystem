

namespace learningManagementSystem.BLL.Service;

public interface IUserDashboardService
{
	Task<CommonResponse> MarkUserAsAdminAsync(string email);
	Task<GetUserDto> GetUserByEmailOrUserNameAsync(string emailOrUserName);
	int GetUsersNumber();
	Task<int> GetStudentsNumberAsync();
	Task<int> GetInstructorsNumberAsync();
	Task<int> GetAdminsCountAsync();
	Task<int> GetBockedUsersCountAsync();
	Task<CommonResponse> BlockUserForPeriodAsync(BlockUserDto model);
	Task<CommonResponse> UnBlockUserForPeriodAsync(string email);
}
