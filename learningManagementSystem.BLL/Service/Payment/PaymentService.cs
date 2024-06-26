
using Stripe;

namespace learningManagementSystem.BLL.Service;

public class PaymentService : IPaymentService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly StripeSettings _stripeSettings;
	private readonly IEmailService _emailService;

	public PaymentService(IUnitOfWork unitOfWork, StripeSettings stripeSettings, IEmailService emailService)
    {
		_unitOfWork = unitOfWork;
		_stripeSettings = stripeSettings;
		_emailService = emailService;
	}

    public async Task<BuyCourseDto> CreateOrUpdatePaymentIntentAsync(CreateOrUpdatePaymentDto model)
	{

		StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

		//> get course payment to access the course which need to buy
		var coursePayment = await _unitOfWork.CoursePaymentRepo.GetByIdAsync(model.CoursePaymentId);
		if (coursePayment is null)
		{
			return null!;
		}
		var courseToBuy = await _unitOfWork.CourseRepo.GetByIdAsync(coursePayment.CourseId);
		if (courseToBuy is null)
		{
			return null!;
		}

		//> get user by email to access the student which buy the course
		var buyer = await _unitOfWork.UserManager.FindByEmailAsync(model.Email);
		if(buyer is null)
		{
			return null!;
		}
		var student = await _unitOfWork.StudentRepo.GetByRefIdAsync(buyer.Id);
		if(student is null)
		{
			return null!;
		}


		PaymentIntentService service = new PaymentIntentService();
		PaymentIntent intent = default!;

		if (string.IsNullOrEmpty(coursePayment.PaymentIntentId))
		{
			var options = new PaymentIntentCreateOptions
			{
				Amount = (long)courseToBuy.OfferOrice * 100,
				Currency = "USD",
				PaymentMethodTypes = new List<string> { "card" }
			};

			intent = await service.CreateAsync(options);

			coursePayment.PaymentIntentId = intent.Id;
			coursePayment.PaymentClientSecret = intent.ClientSecret;

			_unitOfWork.CoursePaymentRepo.Update(coursePayment);
			_unitOfWork.CoursePaymentRepo.SaveChanges();

			//> create order after payment finish
			var newOrder = new Order
			{
				Id = Guid.NewGuid(),
				BuyerEmail = model.Email,
				StudentId = student.Id,
				CourseId = courseToBuy.Id,
				Price = courseToBuy.OfferOrice,
				CleintSecret = coursePayment.PaymentClientSecret,
				PaymentIntentId = coursePayment.PaymentIntentId,
				CreatedAt = DateTime.Now
			};

			await _unitOfWork.OrderRepo.CreateAsync(newOrder);
			await _unitOfWork.OrderRepo.SaveChangesAsync();

		}
		else
		{
			var options = new PaymentIntentUpdateOptions
			{
				Amount = (long) courseToBuy.OfferOrice * 100
			};

			//> get order by paymentIntentId for update the price
			var orderToUpdate = await _unitOfWork.OrderRepo.GetOrderByPaymentInetntIdAsync(coursePayment.PaymentIntentId);
			if(orderToUpdate is not null)
			{
				orderToUpdate.Price = courseToBuy.OfferOrice;

				 _unitOfWork.OrderRepo.Update(orderToUpdate);
				 _unitOfWork.OrderRepo.SaveChanges();
			}

			await service.UpdateAsync(coursePayment.PaymentIntentId, options);
		}

		return new BuyCourseDto
		{
			PaymentIntentId = coursePayment.PaymentIntentId,
			ImgeUrl = courseToBuy.ImgeUrl,
			Price = courseToBuy.OfferOrice,
			PaymentClientSecret = coursePayment.PaymentClientSecret,
			Title = courseToBuy.Title
		};

	}

	public async Task<GetCourseWithIncludesDto> UpdateCourseWhenPaymentFailAsync(string paymentIntentId)
	{
		var course = await _unitOfWork.CoursePaymentRepo.GetCoursePaymentIntentIdAsync(paymentIntentId);
		if (course is null)
		{
			return null!;
		}

		int studentsNumber = _unitOfWork.StudentCourseRepo.GetStudentsNumber(course.Id);

		//> send email payment fail or perform any logic

		return CourseMapper.ToGetWithIncludesDto(course, studentsNumber);
	}

	public async Task<GetCourseWithIncludesDto> UpdateCourseWhenPaymentSuccessAsync(string paymentIntentId, string userEmail)
	{
		var course = await _unitOfWork.CoursePaymentRepo.GetCoursePaymentIntentIdAsync(paymentIntentId);
		if (course is null)
		{
			return null!;
		}

		var user = await _unitOfWork.UserManager.FindByEmailAsync(userEmail);
		if(user is null)
		{
			return null!;
		}

		await _unitOfWork.CourseRepo.UnlockCourseVideosAsync(course.Id);
		int studentsNumber =  _unitOfWork.StudentCourseRepo.GetStudentsNumber(course.Id);

		//> send email payment success
		var emailBody = _emailService.SuccessCourseOrderEmailBody(course, user.DisplayName);
		await _emailService.SendEmailAsync(user.Email, "Course Purchase Success", emailBody, true);

		return CourseMapper.ToGetWithIncludesDto(course, studentsNumber);

	}
}
