
namespace learningManagementSystem.DAL.Repositories;

public interface IOrderRepo : IGenericRepo<Order>
{
	Task<Order> GetByIdWithIncludesAsync(Guid id);
	Task<Order> GetOrderByPaymentInetntIdAsync(string paymentIntentId);
	Task<IEnumerable<Order>> GetAllOrdersForUserAsync(ApplicationUser user);
}
