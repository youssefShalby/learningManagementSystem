
namespace learningManagementSystem.BLL.Service;

public interface IPaymentService
{
	Task<BuyCourseDto> CreateOrUpdatePaymentIntentAsync(Guid coursePaymentId, BuyerDataDto model);
	Task<GetCourseWithIncludesDto> UpdateCourseWhenPaymentFailAsync(string paymentIntentId);
	Task<GetCourseWithIncludesDto> UpdateCourseWhenPaymentSuccessAsync(string paymentIntentId);
}
