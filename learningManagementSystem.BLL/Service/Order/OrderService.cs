


namespace learningManagementSystem.BLL.Service;

public class OrderService : IOrderService, IOrderDashboardService
{
	private readonly IUnitOfWork _unitOfWork;

	public OrderService(IUnitOfWork unitOfWork)
    {
		_unitOfWork = unitOfWork;
	}

	public int GetOrdersCount()
	{
		return _unitOfWork.OrderDashboardRepo.GetOrdersCount();
	}

	public int GetOrdersCountOfLastMonth()
	{
		return _unitOfWork.OrderDashboardRepo.GetOrdersCountOfLastMonth();
	}

	public int GetOrdersCountOfLastYear()
	{
		return _unitOfWork.OrderDashboardRepo.GetOrdersCountOfLastYear();
	}

	public async Task<IEnumerable<GetOrderDto>> GetOrdersOfLastMonthAsync(int pageNumber)
	{
		var orders = await _unitOfWork.OrderRepo.GetOrdersOfLastMonthAsync(pageNumber);

		return orders.Select(order => new GetOrderDto
		{
			BuyerEmail = order.BuyerEmail,
			CreatedAt = order.CreatedAt,
			Price = order.Price,
			CourseName = order.Course?.Title ?? "NA",
			StudentEmail = order.Student?.AppUser?.Email ?? "NA"

		}).ToList();
	}

	public async Task<IEnumerable<GetOrderDto>> GetOrdersOfLastYearAsync(int pageNumber)
	{
		var orders = await _unitOfWork.OrderRepo.GetOrdersOfLastYearAsync(pageNumber);

		return orders.Select(order => new GetOrderDto
		{
			BuyerEmail = order.BuyerEmail,
			CreatedAt = order.CreatedAt,
			Price = order.Price,
			CourseName = order.Course?.Title ?? "NA",
			StudentEmail = order.Student?.AppUser?.Email ?? "NA"

		}).ToList();
	}
}
