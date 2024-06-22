

namespace learningManagementSystem.DAL.Repositories;

public interface ICourseRepo : IGenericRepo<Course>
{
	Task<IEnumerable<Course>> GetAllWithQueryAsync(CourseQueryHandler query);
	Task<Course> GetByIdWithIncludesAsync(Guid id);
	Task<IEnumerable<Course>> GetInstructorCoursesAsync(ApplicationUser user);
}
