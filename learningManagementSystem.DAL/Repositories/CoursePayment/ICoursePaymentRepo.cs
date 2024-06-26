

namespace learningManagementSystem.DAL.Repositories;

public interface ICoursePaymentRepo : IGenericRepo<CoursePayment>
{
	Task<Course> GetCoursePaymentIntentIdAsync(string paymentIntentId);
}
