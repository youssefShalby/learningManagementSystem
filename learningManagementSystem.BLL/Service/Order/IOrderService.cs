

namespace learningManagementSystem.BLL.Service;

public interface IOrderService
{
	Task<IEnumerable<GetOrderDto>> GetOrdersOfLastMonthAsync(int pageNumber);
	Task<IEnumerable<GetOrderDto>> GetOrdersOfLastYearAsync(int pageNumber);
}
