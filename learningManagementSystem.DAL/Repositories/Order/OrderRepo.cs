



namespace learningManagementSystem.DAL.Repositories;

public class OrderRepo : GenericRepo<Order>, IOrderRepo
{
	private readonly AppDbContext _context;

	public OrderRepo(AppDbContext context, IConfiguration configuration) : base(context, configuration)
    {
		_context = context;
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
}
