

namespace learningManagementSystem.DAL.Repositories;

public interface ICourseRepo : IGenericRepo<Course>
{
	Task<IEnumerable<Course>> GetAllWithQueryAsync(CourseQueryHandler query);
	Task<Course> GetByIdWithIncludesAsync(Guid id);
	Task<IEnumerable<Course>> GetInstructorCoursesAsync(CourseQueryHandler query, Guid instructorId);
	Task UnlockCourseVideosAsync(Guid id);
	Task DeleteInstrutorCourses(Guid instructorId);
	Task<IEnumerable<Course>> GetCoursesOfLastMonthAsync(int pageNumber);
	Task<IEnumerable<Course>> GetCoursesOfLastYearAsync(int pageNumber);
	bool IsInstructorOfCourse(Guid userId, Guid courseId);
	int GetInstructorCoursesCount(Guid instructordId);
	
}
