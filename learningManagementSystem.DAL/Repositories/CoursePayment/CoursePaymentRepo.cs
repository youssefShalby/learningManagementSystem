




namespace learningManagementSystem.DAL.Repositories;

public class CoursePaymentRepo : GenericRepo<CoursePayment>, ICoursePaymentRepo
{
	private readonly AppDbContext _context;

	public CoursePaymentRepo(AppDbContext context, IConfiguration configuration) : base(context, configuration)
    {
		_context = context;
	}

	public async Task<Course> GetCoursePaymentIntentIdAsync(string paymentIntentId)
	{
		var coursePayment = await _context.CoursePayments.FirstOrDefaultAsync(CP => CP.PaymentIntentId == paymentIntentId);
		if(coursePayment is null)
		{
			return null!;
		}

		return await _context.Set<Course>()
		.Include(c => c.Comments!).ThenInclude(c => c.AppUser)
		.Include(c => c.Advanteges)
		.Include(c => c.Category)
		.Include(c => c.Instructor).ThenInclude(I => I!.AppUser)
		.Include(c => c.Lessons!).ThenInclude(L => L.Videos)
		.FirstOrDefaultAsync(c => c.Id == coursePayment.CourseId) ?? null!;
	}
}
