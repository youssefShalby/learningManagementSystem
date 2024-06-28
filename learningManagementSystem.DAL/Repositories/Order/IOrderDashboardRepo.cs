

namespace learningManagementSystem.DAL.Repositories;

public interface IOrderDashboardRepo
{
	public int GetOrdersCountOfLastYear();
	public int GetOrdersCountOfLastMonth();
	public int GetOrdersCount();
}
