


namespace learningManagementSystem.BLL.Service;

public class CoursePaymentService : ICoursePaymentService
{
	private readonly ICoursePaymentRepo _coursePaymentRepo;

	public CoursePaymentService(ICoursePaymentRepo coursePaymentRepo)
    {
		_coursePaymentRepo = coursePaymentRepo;
	}

	public async Task<CommonResponse> CreateCoursePaymentAsync(CreateCoursePaymentDto model)
	{
		try
		{
			CoursePayment newCoursePayment = new CoursePayment
			{
				CourseId = model.CourseId,
				Id = Guid.NewGuid(),
				PaymentClientSecret = model.PaymentClientSecret,
				PaymentIntentId	= model.PaymentIntentId,
			};

			await _coursePaymentRepo.CreateAsync(newCoursePayment);
			await _coursePaymentRepo.SaveChangesAsync();
			return new CommonResponse("course payment created..!!", true);
		}
		catch(Exception ex)
		{
			return new CommonResponse($"cannot create course paymnet because {ex.Message}", false);
		}
	}

	public async Task<CommonResponse> DeleteCoursePaymentAsync(Guid id)
	{
		var coursePaymentToDelete = await _coursePaymentRepo.GetByIdAsync(id);
		if(coursePaymentToDelete is null)
		{
			return new CommonResponse("course payment not found..!!", false);
		}

		try
		{
			_coursePaymentRepo.Delete(coursePaymentToDelete);
			_coursePaymentRepo.SaveChanges();
			return new CommonResponse("deleted..!!", true);
		}
		catch (Exception ex)
		{
			return new CommonResponse($"cannot delete course paymnet because {ex.Message}", false);
		}
	}

	public async Task<GetCoursePaymentDto> GetByIdAsync(Guid id)
	{
		var coursePayment = await _coursePaymentRepo.GetByIdAsync(id);
		if (coursePayment is null)
		{
			return null!;
		}

		return new GetCoursePaymentDto
		{
			CourseId = coursePayment.CourseId,
			PaymentClientSecret = coursePayment.PaymentClientSecret,
			PaymentIntentId = coursePayment.PaymentIntentId,
		};
	}

	public async Task<CommonResponse> UpdateCoursePaymentAsync(Guid id, UpdateCoursePaymentDto model)
	{
		var coursePaymentToUpdate = await _coursePaymentRepo.GetByIdAsync(id);
		if (coursePaymentToUpdate is null)
		{
			return new CommonResponse("course payment not found..!!", false);
		}

		try
		{
			coursePaymentToUpdate.PaymentIntentId = model.PaymentIntentId;
			coursePaymentToUpdate.PaymentClientSecret = model.PaymentClientSecret;
			coursePaymentToUpdate.CourseId = model.CourseId;

			_coursePaymentRepo.Update(coursePaymentToUpdate);
			_coursePaymentRepo.SaveChanges();
			return new CommonResponse("updated..!!", true);
		}
		catch (Exception ex)
		{
			return new CommonResponse($"cannot update course paymnet because {ex.Message}", false);
		}
	}
}
