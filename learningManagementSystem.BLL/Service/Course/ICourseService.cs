

namespace learningManagementSystem.BLL.Service;

public interface ICourseService
{
	Task<CommonResponse> UnlockVideos(Guid id);
	Task<CommonResponse> CreateCourseAsync(CreateCourseDto model, string userId);
	Task<CommonResponse> UpdateCourseAsync(Guid id, UpdateCourseDto model);
	Task<CommonResponse> DeleteCourseAsync(Guid id);
	Task<CommonResponse> EnrollStudentToCourseAsync(EnrollStudentToCourseDto model);
	Task<CommonResponse> OutStudentFromCourseAsync(OutUserFromCourseDto model);
	Task<CommonResponse> MarkCourseAsDeletedAsync(Guid id);


}
