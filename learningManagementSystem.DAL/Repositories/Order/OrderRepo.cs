



namespace learningManagementSystem.DAL.Repositories;

public class OrderRepo : GenericRepo<Order>, IOrderRepo, IOrderDashboardRepo
{
	private readonly AppDbContext _context;
	private readonly IConfiguration _configuration;

	public OrderRepo(AppDbContext context, IConfiguration configuration) : base(context, configuration)
    {
		_context = context;
		_configuration = configuration;
	}

	public async Task<IEnumerable<Order>> GetAllOrdersForUserAsync(ApplicationUser user)
	{
		var orders = _context.Orders.Where(O => O.Student!.UserRefId == user.Id);
		return await orders.Select(order => new Order
		{
			Id = order.Id,
			Student = order.Student,
			BuyerEmail = order.BuyerEmail,
			CleintSecret = order.CleintSecret,
			Course = order.Course,
			CourseId = order.CourseId,
			PaymentIntentId = order.PaymentIntentId,
			Price = order.Price, StudentId = order.StudentId,

		}).ToListAsync();

	}

	public async Task<IEnumerable<Order>> GetOrdersOfLastMonthAsync(int pageNumber)
	{
		var orders =  _context.Orders
			.Include(o => o.Course)
			.Include(o => o.Student).ThenInclude(s => s!.AppUser)
			.Where(O => O.CreatedAt >= DateTime.Now.AddMonths(-1)).AsQueryable();

		var pageSize = int.Parse(_configuration["CustomConfiguration:PageSize"] ?? "10");

		orders = orders.Skip((pageNumber - 1) * pageSize).Take(pageSize);
		return await orders.ToListAsync();
	} 
	
	public async Task<IEnumerable<Order>> GetOrdersOfLastYearAsync(int pageNumber)
	{
		var orders = _context.Orders
			.Include(o => o.Course)
			.Include(o => o.Student).ThenInclude(s => s!.AppUser)
			.Where(O => O.CreatedAt >= DateTime.Now.AddYears(-1)).AsQueryable();

		var pageSize = int.Parse(_configuration["CustomConfiguration:PageSize"] ?? "10");

		orders = orders.Skip((pageNumber - 1) * pageSize).Take(pageSize);
		return await orders.ToListAsync();
	}

	public int GetOrdersCountOfLastYear()
	{
		return _context.Set<Order>()
			.Where(c => c.CreatedAt >= DateTime.Now.AddYears(-1)).Count();
	}

	public int GetOrdersCountOfLastMonth()
	{
		return _context.Set<Order>()
			.Where(c => c.CreatedAt >= DateTime.Now.AddMonths(-1)).Count();
	}

	public int GetOrdersCount()
	{
		return _context.Orders.Count();
	}

	public async Task<Order> GetByIdWithIncludesAsync(Guid id)
	{
		return await _context.Orders
			.Include(O => O.Course)
			.FirstOrDefaultAsync(O => O.Id == id) ?? null!;
	}

	public async Task<Order> GetOrderByPaymentInetntIdAsync(string paymentIntentId)
	{
		return await _context.Orders.FirstOrDefaultAsync(o => o.PaymentIntentId == paymentIntentId) ?? null!;
	}

	public bool HasPurchasedCourse(Guid userId, Guid courseId)
	{
		return _context.Orders.Any(cp => cp.CourseId == courseId && cp.StudentId == userId);
	}
}
