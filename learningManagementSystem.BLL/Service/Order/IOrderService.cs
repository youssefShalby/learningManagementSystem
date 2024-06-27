

namespace learningManagementSystem.BLL.Service;

public interface IOrderService
{
	Task<IEnumerable<GetOrderDto>> GetOrdersOfLastMonthAsync(int pageNumber);
	Task<IEnumerable<GetOrderDto>> GetOrdersOfLastYearAsync(int pageNumber);

	public int GetOrdersCountOfLastYear();
	public int GetOrdersCountOfLastMonth();
	public int GetOrdersCount();
}
