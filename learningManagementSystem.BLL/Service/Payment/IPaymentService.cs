
namespace learningManagementSystem.BLL.Service;

public interface IPaymentService
{
	Task<BuyCourseDto> CreateOrUpdatePaymentIntentAsync(CreateOrUpdatePaymentDto model);
	Task<GetCourseWithIncludesDto> UpdateCourseWhenPaymentFailAsync(string paymentIntentId);
	Task<GetCourseWithIncludesDto> UpdateCourseWhenPaymentSuccessAsync(string paymentIntentId);
}
