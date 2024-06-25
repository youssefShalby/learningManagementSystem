


namespace learningManagementSystem.DAL.Repositories;

public class StudentRepo : GenericRepo<Student>, IStudentRepo
{
	private readonly AppDbContext _context;

	public StudentRepo(AppDbContext context, IConfiguration configuration) : base(context, configuration)
    {
		_context = context;
	}

	public async Task<Student> GetByRefIdAsync(string refId)
	{
		return await _context.Students.FirstOrDefaultAsync(S => S.UserRefId == refId) ?? null!;
	}
}
