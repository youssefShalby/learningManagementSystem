


namespace learningManagementSystem.BLL.Service;

public class OrderService : IOrderService
{
	private readonly IUnitOfWork _unitOfWork;

	public OrderService(IUnitOfWork unitOfWork)
    {
		_unitOfWork = unitOfWork;
	}

	public int GetOrdersCount()
	{
		return _unitOfWork.OrderRepo.GetOrdersCount();
	}

	public int GetOrdersCountOfLastMonth()
	{
		return _unitOfWork.OrderRepo.GetOrdersCountOfLastMonth();
	}

	public int GetOrdersCountOfLastYear()
	{
		return _unitOfWork.OrderRepo.GetOrdersCountOfLastYear();
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
