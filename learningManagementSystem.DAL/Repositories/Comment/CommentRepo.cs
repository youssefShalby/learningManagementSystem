


namespace learningManagementSystem.DAL.Repositories;

public class CommentRepo : GenericRepo<Comment>, ICommentRepo
{
	private readonly AppDbContext _context;

	public CommentRepo(AppDbContext context, IConfiguration configuration) : base(context, configuration)
    {
		_context = context;
	}

	public async Task<Comment> GetCommentWithRpliesAsync(Guid id)
	{
		return await _context.Comments
			.Include(c => c.Replies!)
			.ThenInclude(cr => cr.AppUser)
			.FirstOrDefaultAsync(c => c.Id == id) ?? null!;
	}
}
