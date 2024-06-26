
namespace learningManagementSystem.BLL.Service;

public interface ICoursePaymentService
{
	Task<CommonResponse> CreateCoursePaymentAsync(CreateCoursePaymentDto model);
	Task<CommonResponse> UpdateCoursePaymentAsync(Guid id, UpdateCoursePaymentDto model);
	Task<CommonResponse> DeleteCoursePaymentAsync(Guid id);
	Task<GetCoursePaymentDto> GetByIdAsync(Guid id);
}
