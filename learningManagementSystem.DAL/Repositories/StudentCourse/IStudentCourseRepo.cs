

namespace learningManagementSystem.DAL.Repositories;

public interface IStudentCourseRepo
{
	Task<IEnumerable<Course>> GetStudentCoursesAsync(CourseQueryHandler query, Guid studentId);
	int GetStudentsNumber(Guid courseId);
	int GetStudentCoursesCount(Guid studentId);
}
