


using MailKit.Net.Smtp;
using MimeKit;

namespace learningManagementSystem.BLL.Service;

public class EmailService : IEmailService
{
	private readonly SmtpSettings _smtpSettings;

	public EmailService(SmtpSettings smtpSettings)
    {
		_smtpSettings = smtpSettings;
	}

    public string GenerateOTPCode()
	{
		return new Random().Next(1000, 9999).ToString();
	}

	public async Task<CommonResponse> SendEmailAsync(string toEmail, string subject, string body, bool isHtml = false)
	{
		MimeMessage emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
        emailMessage.To.Add(new MailboxAddress("", toEmail));
        emailMessage.Subject = subject;

        var bodyBuilde = new BodyBuilder();

        if (isHtml)
        {
            bodyBuilde.HtmlBody = body;
        }
        else
        {
            bodyBuilde.TextBody = body;
        }

        emailMessage.Body = bodyBuilde.ToMessageBody();

        using (var client = new SmtpClient())
        {
            try
            {
                await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);

                return new CommonResponse("email sened..!!", true);
            }
            catch(Exception ex)
            {
				return new CommonResponse($"cannot send email rigth now because: {ex.Message}", false);

			}
		}
	}

	public string GetConfirmationEmailBody(string otp, string userName = "User")
	{
		return @$"
				<!DOCTYPE html>
				<html lang=""en"">
				<head>
					<meta charset=""UTF-8"">
					<title>Confirmation Email</title>
				</head>
				<body>
					<p>Hi {userName},</p>
					<p>Thank you for signing up with Company Name. To complete your account verification, enter the following One-Time Password (OTP) code:</p>
					<p style=""font-weight: bold; font-size: 16px;"">{otp}</p>
					<p>This code is valid for [duration] minutes. Please do not share this code with anyone.</p>
					<p>If you did not initiate this request, please disregard this email and contact our support team.</p>
					<p>Sincerely,</p>
					<p>E-CommerceApp</p>
				</body>
				</html>
				";
	}
	public string ResetPasswordEmailBody(string url)
	{
		return @$"<!DOCTYPE html>
                <html lang=""en"">
                <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Password Reset</title>
                <style>
                    /* Reset CSS */
                    body, html {{
                    margin: 0;
                    padding: 0;
                    font-family: Arial, sans-serif;
                    }}
                    /* Wrapper */
                    .wrapper {{
                    max-width: 600px;
                    margin: 0 auto;
                    padding: 20px;
                    }}
                    /* Header */
                    .header {{
                    text-align: center;
                    margin-bottom: 20px;
                    }}
                    /* Content */
                    .content {{
                    background-color: #f9f9f9;
                    padding: 20px;
                    border-radius: 5px;
                    }}
                    /* Button */
                    .button {{
                    display: inline-block;
                    background-color: #007bff;
                    color: #fff;
                    text-decoration: none;
                    padding: 10px 20px;
                    border-radius: 5px;
                    }}
                </style>
                </head>
                <body>
                <div class=""wrapper"">
                    <div class=""header"">
                    <h1>Password Reset</h1>
                    </div>
                    <div class=""content"">
                    <p>You have requested a password reset. Click the button below to reset your password:</p>
                    <p><a class=""button"" href='{url}'>Reset Password</a></p>
                    <p>If you didn't request a password reset, you can safely ignore this email.</p>
                    </div>
                </div>
                </body>
                </html>
";
	}

	public string SuccessCourseOrderEmailBody(Course course, string UserName, int studentsNumber)
    {
        return @$"

                <!DOCTYPE html>
                <html lang=""en"">
                <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Course Purchase Success</title>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        background-color: #f4f4f4;
                        color: #333;
                        margin: 0;
                        padding: 0;
                    }}
                    .container {{
                        width: 100%;
                        max-width: 600px;
                        margin: 0 auto;
                        background-color: #fff;
                        padding: 20px;
                        border-radius: 10px;
                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                    }}
                    .header {{
                        background-color: #4caf50;
                        color: #fff;
                        padding: 10px 0;
                        text-align: center;
                        border-radius: 10px 10px 0 0;
                    }}
                    .content {{
                        padding: 20px;
                    }}
                    .course-details {{
                        margin: 20px 0;
                    }}
                    .footer {{
                        background-color: #f4f4f4;
                        color: #777;
                        text-align: center;
                        padding: 10px;
                        border-radius: 0 0 10px 10px;
                        font-size: 0.9em;
                    }}
                </style>
            </head>
            <body>
                <div class=""container"">
                    <div class=""header"">
                        <h1>Course Purchase Success</h1>
                    </div>
                    <div class=""content"">
                        <p>Dear {UserName},</p>
                        <p>Congratulations! You have successfully purchased the course:</p>
                        <div class=""course-details"">
                            <h2>{course.Title}</h2>
                            <p><strong>Instructor:</strong> {course.Instructor?.AppUser?.DisplayName ?? "Admin"} </p>
                            <p><strong>Price:</strong> {course.OfferOrice} </p>
                            <p><strong>students count :</strong> {studentsNumber} </p>
                            <p><strong>Lessons count :</strong> {course.Lessons?.Count() ?? 0} </p>
                        </div>
                        <p>We hope you enjoy the course and find it valuable. If you have any questions or need support, please feel free to contact us.</p>
                    </div>
                    <div class=""footer"">
                        <p>&copy; {DateTime.Now.Year} Dev:Youssef Shalaby. All rights reserved.</p>
                    </div>
                </div>
            </body>
            </html>


                ";
    }
}
