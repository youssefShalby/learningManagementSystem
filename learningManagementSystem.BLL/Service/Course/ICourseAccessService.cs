

namespace learningManagementSystem.BLL.Service;

public interface ICourseAccessService
{
	Task<IEnumerable<GetCourseWithCategoryDto>> GetAllToShowAsync(int pageNumber);
	Task<IEnumerable<GetCourseWithCategoryDto>> GetAllWithQueryAsync(CourseQueryHandler query);
	Task<IEnumerable<GetCourseWithCategoryDto>> GetInstructorCoursesAsync(CourseQueryHandler query, string userId);
	Task<IEnumerable<GetCourseWithCategoryDto>> GetStudentCoursesAsync(CourseQueryHandler query, string userId);
	Task<GetCourseWithIncludesDto> GetByIdWithIncludesAsync(Guid id);
}
