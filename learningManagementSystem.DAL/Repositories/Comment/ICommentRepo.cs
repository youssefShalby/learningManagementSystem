

namespace learningManagementSystem.DAL.Repositories;

public interface ICommentRepo : IGenericRepo<Comment>	
{
	Task<Comment> GetCommentWithRpliesAsync(Guid id);
}
