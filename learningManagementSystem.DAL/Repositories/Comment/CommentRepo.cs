

namespace learningManagementSystem.DAL.Repositories;

public class CommentRepo : GenericRepo<Comment>, ICommentRepo
{
    public CommentRepo(AppDbContext context, IConfiguration configuration) : base(context, configuration)
    {
        
    }
}
