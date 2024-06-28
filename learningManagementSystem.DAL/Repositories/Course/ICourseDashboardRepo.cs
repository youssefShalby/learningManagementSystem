
namespace learningManagementSystem.DAL.Repositories;

public interface ICourseDashboardRepo
{
	public int GetCoursesCountOfLastYear();
	public int GetCoursesCountOfLastMonth();
	public int GetCoursesCount();
}
