

namespace learningManagementSystem.DAL.Repositories;

public interface IStudentCourseRepo : IGenericRepo<StudentCourse>
{
	Task<IEnumerable<Course>> GetStudentCoursesAsync(CourseQueryHandler query, Guid studentId);
	Task<StudentCourse> GetByStudentIdAndCourseIdAsync(Guid courseId, Guid studentId);
	Task<IEnumerable<StudentCourse>> CheckIfThereAreStudentsOrNotAsync(Guid courseId);
	int GetStudentsNumber(Guid courseId);
	int GetStudentCoursesCount(Guid studentId);
}
