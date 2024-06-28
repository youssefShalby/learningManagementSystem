
namespace learningManagementSystem.DAL.Repositories;

public interface IOrderRepo : IGenericRepo<Order>
{
	Task<Order> GetByIdWithIncludesAsync(Guid id);
	Task<Order> GetOrderByPaymentInetntIdAsync(string paymentIntentId);
	Task<IEnumerable<Order>> GetAllOrdersForUserAsync(ApplicationUser user);
	bool HasPurchasedCourse(Guid userId, Guid courseId);
	Task<IEnumerable<Order>> GetOrdersOfLastMonthAsync(int pageNumber);
	Task<IEnumerable<Order>> GetOrdersOfLastYearAsync(int pageNumber);
}
