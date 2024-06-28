

namespace learningManagementSystem.BLL.Service;

public interface ICourseDashboardService
{
	Task<IEnumerable<GetCourseToAdminDashDto>> GetCoursesOfLastMonthAsync(int pageNumber);
	Task<IEnumerable<GetCourseToAdminDashDto>> GetCoursesOfLastYearAsync(int pageNumber);
	public int GetCoursesCountOfLastYear();
	public int GetCoursesCountOfLastMonth();
	public int GetCoursesCount();
}
