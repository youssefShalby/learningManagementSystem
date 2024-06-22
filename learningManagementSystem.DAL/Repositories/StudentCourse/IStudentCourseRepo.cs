

namespace learningManagementSystem.DAL.Repositories;

public interface IStudentCourseRepo
{
	Task<IEnumerable<Course>> GetStudentCoursesAsync(ApplicationUser user);
}
