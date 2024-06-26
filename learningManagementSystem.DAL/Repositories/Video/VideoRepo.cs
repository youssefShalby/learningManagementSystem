


namespace learningManagementSystem.DAL.Repositories;

public class VideoRepo : GenericRepo<Video>, IVideoRepo
{
	private readonly AppDbContext _context;

	public VideoRepo(AppDbContext context, IConfiguration configuration) : base(context, configuration)
    {
		_context = context;
	}

	public async Task<Video> GetByIdWithIncludesAsync(Guid id)
	{
		return await _context.Videos
			.Include(V => V.Comments!).ThenInclude(C => C.AppUser)
			.Include(V => V.Lesson)
			.FirstOrDefaultAsync(V => V.Id == id) ?? null!;
	}
}
