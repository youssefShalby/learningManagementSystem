
namespace learningManagementSystem.BLL.Service;

public interface IOrderDashboardService
{
	public int GetOrdersCountOfLastYear();
	public int GetOrdersCountOfLastMonth();
	public int GetOrdersCount();
}
