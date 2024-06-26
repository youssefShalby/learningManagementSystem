

namespace learningManagementSystem.BLL.Service;

public interface IVideoService
{
	Task<CommonResponse> UploadVideosAsync(IEnumerable<UploadVideoDto> videos);
	Task<CommonResponse> UpdateVideoAsync(Guid id, UpdateVideoDto model);
	Task<CommonResponse> DeleteVideoAsync(Guid id);
	Task<GetVideoByIdWithIncludesDto> GetByIdWithIncludes(Guid id);
	Task<CommonResponse> LockOrUnlockAsync(Guid id)
}
