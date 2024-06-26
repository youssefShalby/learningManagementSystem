﻿

namespace learningManagementSystem.BLL.DTOs;

public class CreateCoursePaymentDto
{
	public Guid CourseId { get; set; }
	public string PaymentIntentId { get; set; } = string.Empty;
	public string PaymentClientSecret { get; set; } = string.Empty;
}