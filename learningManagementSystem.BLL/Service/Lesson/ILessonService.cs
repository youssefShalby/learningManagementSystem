

namespace learningManagementSystem.BLL.Service;

public interface ILessonService
{
	Task<CommonResponse> CreateLessonAsync(CreateLessonDto model);
	Task<CommonResponse> UpdateLessonAsync(Guid id, UpdateLessonDto model);
	Task<CommonResponse> DeleteLessonAsync(Guid id);
	Task<CommonResponse> MarkLessonAsDeletedAsync(Guid id);
	Task<IEnumerable<GetVideoForLessonDto>> GetVideosOfLessonAsync(Guid id);
}
