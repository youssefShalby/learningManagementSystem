

namespace learningManagementSystem.BLL.Service;

public interface IOrderService
{
	Task<CommonResponse> CreateOrderAsync(CreateOrderDto model);
}

