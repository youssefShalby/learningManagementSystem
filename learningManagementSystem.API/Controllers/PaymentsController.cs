﻿
using Microsoft.AspNetCore.Authorization;
using Stripe;

namespace learningManagementSystem.API.Controllers;



[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
	private readonly IPaymentService _paymentService;
	private readonly StripeSettings _stripeSettings;
	private readonly string _webHooks;

	public PaymentsController(IPaymentService paymentService, StripeSettings stripeSettings)
    {
		_paymentService = paymentService;
		_stripeSettings = stripeSettings;
		_webHooks = _stripeSettings.WebHooksSecret;
	}

	[HttpPost("CreatePaymentIntent")]
	public async Task<ActionResult> CreateOrUpdatePaymentIntent(CreateOrUpdatePaymentDto model)
	{
		var result = await _paymentService.CreateOrUpdatePaymentIntentAsync(model);
		if(result is null)
		{
			return BadRequest("something went error..!!");
		}
		return Ok(result);
	}

	[HttpPost("webhook")]
	public async Task<ActionResult> StripeWebHook()
	{
		var json = await new StreamReader(Request.Body).ReadToEndAsync();
		var stripeSignature = Request.Headers["Stripe-Signature"];

		if (string.IsNullOrEmpty(stripeSignature) || string.IsNullOrEmpty(json))
		{
			return NoContent();
		}

		var stripeEvent = EventUtility.ConstructEvent(json, stripeSignature, _webHooks);

		PaymentIntent intent;
		GetCourseWithIncludesDto course;

		switch (stripeEvent.Type)
		{
			case "payment_intent.succeeded":

				intent = (PaymentIntent)stripeEvent.Data.Object;

				course = await _paymentService.UpdateCourseWhenPaymentSuccessAsync(intent.Id);
				break;

			case "payment_intent.payment_failed":
				intent = (PaymentIntent)stripeEvent.Data.Object;
				course = await _paymentService.UpdateCourseWhenPaymentFailAsync(intent.Id);
				break;
		}

		return NoContent();
	}
}
