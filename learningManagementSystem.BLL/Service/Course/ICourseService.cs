

namespace learningManagementSystem.BLL.Service;

public interface ICourseService
{
	Task<IEnumerable<GetCourseWithCategoryDto>> GetAllToShowAsync(int pageNumber);
	Task<IEnumerable<GetCourseWithCategoryDto>> GetAllWithQueryAsync(CourseQueryHandler query);
	Task<IEnumerable<GetCourseWithCategoryDto>> GetInstructorCoursesAsync(CourseQueryHandler query, string userId);
	Task<IEnumerable<GetCourseWithCategoryDto>> GetStudentCoursesAsync(CourseQueryHandler query, string userId);
	Task<GetCourseWithIncludesDto> GetByIdWithIncludesAsync(Guid id);
	Task<CommonResponse> UnlockVideos(Guid id);
	Task<CommonResponse> CreateCourseAsync(CreateCourseDto model, string userId);
	Task<CommonResponse> UpdateCourseAsync(Guid id, UpdateCourseDto model);
	Task<CommonResponse> DeleteCourseAsync(Guid id);
	Task<CommonResponse> EnrollStudentToCourseAsync(EnrollStudentToCourseDto model);
	Task<CommonResponse> OutStudentFromCourseAsync(OutUserFromCourseDto model);

	//> admin dashbaoard endpoints services
	Task<CommonResponse> MarkCourseAsDeletedAsync(Guid id);
	Task<IEnumerable<GetCourseToAdminDashDto>> GetCoursesOfLastMonthAsync(int pageNumber);
	Task<IEnumerable<GetCourseToAdminDashDto>> GetCoursesOfLastYearAsync(int pageNumber);
	public int GetCoursesCountOfLastYear();
	public int GetCoursesCountOfLastMonth();
	public int GetCoursesCount();

}
