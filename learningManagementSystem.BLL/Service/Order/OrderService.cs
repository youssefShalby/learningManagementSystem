


namespace learningManagementSystem.BLL.Service;

public class OrderService : IOrderService
{
	private readonly IUnitOfWork _unitOfWork;

	public OrderService(IUnitOfWork unitOfWork)
    {
		_unitOfWork = unitOfWork;
	}

	public async Task<CommonResponse> CreateOrderAsync(CreateOrderDto model)
	{
		var newOrder = new Order
		{
			BuyerEmail = model.BuyerEmail,
			CleintSecret = model.CleintSecret,
			PaymentIntentId = model.PaymentIntentId,
			Price = model.Price,
			CourseId = model.CourseId,
			StudentId = model.StudentId,
			Id = Guid.NewGuid(),
			CreatedAt = DateTime.Now
		};

		try
		{
			await _unitOfWork.OrderRepo.CreateAsync(newOrder);
			await _unitOfWork.OrderRepo.SaveChangesAsync();
			return new CommonResponse("", true);
		}
		catch
		{
			return new CommonResponse("", false);
		}
	}
}
