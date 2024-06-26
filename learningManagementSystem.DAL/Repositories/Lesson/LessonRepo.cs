


namespace learningManagementSystem.DAL.Repositories;

public class LessonRepo : GenericRepo<Lesson>, ILessonRepo
{
	private readonly AppDbContext _context;

	public LessonRepo(AppDbContext context, IConfiguration configuration) : base(context, configuration)
    {
		_context = context;
	}

	public async Task<Lesson> GetByIdWithIncludesAsync(Guid id)
	{
		var lesson = await _context.Lessons
			.Include(L => L.Videos)
			.Include(L => L.Course)
			.FirstOrDefaultAsync(x => x.Id == id);

		if(lesson is null || lesson.IsDeleted)
		{
			return null!;
		}
		return lesson;
	}
}
