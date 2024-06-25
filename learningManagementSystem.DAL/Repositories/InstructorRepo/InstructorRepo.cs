



namespace learningManagementSystem.DAL.Repositories;

public class InstructorRepo : GenericRepo<Instructor>, IInstructorRepo
{
	private readonly AppDbContext _context;

	public InstructorRepo(AppDbContext context, IConfiguration configuration) : base(context, configuration)
	{
		_context = context;
	}

	public async Task<Instructor> GetByRefIdAsync(string refId)
	{
		return await _context.Instructors.FirstOrDefaultAsync(I => I.UserRefId == refId) ?? null!;
	}
}
