

namespace learningManagementSystem.BLL.Service;

public interface ICourseService
{
	Task<IEnumerable<GetCourseWithCategoryDto>> GetAllToShowAsync(int pageNumber);
	Task<IEnumerable<GetCourseWithCategoryDto>> GetAllWithQueryAsync(CourseQueryHandler query);
	Task<IEnumerable<GetCourseWithCategoryDto>> GetInstructorCoursesAsync(CourseQueryHandler query, string userId);
	Task<IEnumerable<GetCourseWithCategoryDto>> GetStudentCoursesAsync(CourseQueryHandler query, string userId);
	Task<GetCourseWithIncludesDto> GetByIdWithIncludesAsync(Guid id);
	Task<CommonResponse> UnlockVideos(Guid id);
	Task<CommonResponse> CreateCourseAsync(CreateCourseDto model);
	Task<CommonResponse> UpdateCourseAsync(Guid id, UpdateCourseDto model);
	Task<CommonResponse> DeleteCourseAsync(Guid id);
	Task<CommonResponse> MarkCourseAsDeletedAsync(Guid id);
}
