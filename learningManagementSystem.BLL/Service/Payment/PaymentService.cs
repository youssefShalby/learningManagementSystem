
using Stripe;

namespace learningManagementSystem.BLL.Service;

public class PaymentService : IPaymentService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly StripeSettings _stripeSettings;
	private readonly IOrderService _orderServie;

	public PaymentService(IUnitOfWork unitOfWork, StripeSettings stripeSettings, IOrderService orderService)
    {
		_unitOfWork = unitOfWork;
		_stripeSettings = stripeSettings;
		_orderServie = orderService;
	}

    public async Task<BuyCourseDto> CreateOrUpdatePaymentIntentAsync(Guid coursePaymentId, BuyerDataDto model)
	{

		StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

		var coursePayment = await _unitOfWork.CoursePaymentRepo.GetByIdAsync(coursePaymentId);
		if (coursePayment is null)
		{
			return null!;
		}

		var courseToBuy = await _unitOfWork.CourseRepo.GetByIdAsync(coursePayment.CourseId);
		if (courseToBuy is null)
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

		}
		else
		{
			var options = new PaymentIntentUpdateOptions
			{
				Amount = (long)courseToBuy.OfferOrice * 100
			};

			var orderToUpdate = new UpdateOrderDto
			{
				Price = courseToBuy.OfferOrice
			};

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

		//> send email payment fail

		return CourseMapper.ToGetWithIncludesDto(course, studentsNumber);
	}

	public async Task<GetCourseWithIncludesDto> UpdateCourseWhenPaymentSuccessAsync(string paymentIntentId)
	{
		var course = await _unitOfWork.CoursePaymentRepo.GetCoursePaymentIntentIdAsync(paymentIntentId);
		if (course is null)
		{
			return null!;
		}

		await _unitOfWork.CourseRepo.UnlockCourseVideosAsync(course.Id);
		int studentsNumber =  _unitOfWork.StudentCourseRepo.GetStudentsNumber(course.Id);

		//> send email payment success

		return CourseMapper.ToGetWithIncludesDto(course, studentsNumber);

	}
}
