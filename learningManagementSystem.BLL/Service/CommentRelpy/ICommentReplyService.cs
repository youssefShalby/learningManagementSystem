

namespace learningManagementSystem.BLL.Service;

public interface ICommentReplyService
{
	Task<CommonResponse> CreateCommentReplyAsync(CreateCommentReplyDto model);
	Task<CommonResponse> UpdateCommentReplyAsync(Guid id, UpdateCommentReplyDto model);
	Task<CommonResponse> DeleteCommentReplyAsync(Guid id);
}
