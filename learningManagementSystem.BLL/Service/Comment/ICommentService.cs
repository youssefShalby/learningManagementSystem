


namespace learningManagementSystem.BLL.Service;

public interface ICommentService
{
	Task<CommonResponse> CreateCommentForCourseAsync(CreateCommentForCourseDto model);
	Task<CommonResponse> CreateCommentForVideoAsync(CreateCommentForVideoDto model);
	Task<CommonResponse> UpdateCommentAsync(Guid id, UpdateCommentDto model);
	Task<CommonResponse> DeleteCommentAsync(Guid id);
	Task<IEnumerable<GetRepliesDto>> GetCommentRepliesAsync(Guid id);
}
