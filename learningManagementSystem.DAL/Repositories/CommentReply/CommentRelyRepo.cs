
namespace learningManagementSystem.DAL.Repositories;

public class CommentRelyRepo : GenericRepo<CommentReply>, ICommentRelyRepo
{
    public CommentRelyRepo(AppDbContext context, IConfiguration configuration) : base(context, configuration)
    {
        
    }
}
