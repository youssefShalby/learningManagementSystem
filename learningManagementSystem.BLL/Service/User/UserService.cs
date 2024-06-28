

using Microsoft.EntityFrameworkCore;

namespace learningManagementSystem.BLL.Service;

public class UserService : IUserService, IUserDashboardService, IUserAccountManagmentService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IHandlerService _handlerService;
	private readonly IRedisService _redisService;
	private readonly ITokenService _tokenService;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IEmailService _emailService;

	public UserService(IUnitOfWork unitOfWork, IHandlerService handlerService, IRedisService redisService, ITokenService tokenService,
		IHttpContextAccessor httpContextAccessor, IEmailService emailService)
    {
		_unitOfWork = unitOfWork;
		_handlerService = handlerService;
		_redisService = redisService;
		_tokenService = tokenService;
		_httpContextAccessor = httpContextAccessor;
		_emailService = emailService;
	}

	public async Task<CommonResponse> ConfirmEmailAsync(ConfirmEmailDto model)
	{
		var user = await _unitOfWork.UserManager.FindByIdAsync(model.UserId);
		if(user is null)
		{
			return new CommonResponse("user not found..!!", false);
		}

		if (user.LockoutEnd != null)
		{
			return new CommonResponse("user blocked..!!", false);
		}

		if (user.EmailConfirmed)
		{
			return new CommonResponse("the email already confrimed..!!", true);
		}

		var key = Helper.GetFirstFiveCharsFromId(model.UserId);

		var verficationObject = await _redisService.GetDataAsync<VerficationTokenDto>(key);
		var generatedOtp = verficationObject.OtpCode;
		var generatedToken = verficationObject.VerficationToken;

		if(generatedOtp is null || generatedToken is null)
		{
			return new CommonResponse("code expired, order new one..!!", false);
		}

		if (_tokenService.IsTokenExpired(generatedToken))
		{
			return new CommonResponse("code expired, order new one..!!", false);
		}

		if(model.Code.Length != 4 || model.Code != generatedOtp)
		{
			return new CommonResponse("the code is not valid..!!", false);
		}
		else
		{
			user.EmailConfirmed = true;
			var result = await _unitOfWork.UserManager.UpdateAsync(user);
			if (!result.Succeeded)
			{
				return new CommonResponse("cannot confirm account right now, try again later..!", false);
			}

			//> delete token and code from cache
			await _redisService.DeleteDataAsync<VerficationTokenDto>(key);

			var loginToken = await _tokenService.CreateLoginTokenAsync(user);
			_tokenService.SaveTokenInCookie(loginToken, user.Id);

			return new CommonResponse("email confirmed and you logged in now..✔", true);
		}
	}

	public async Task<CommonResponse> ForgetPasswordAsync(string email)
	{
		var user = await _unitOfWork.UserManager.FindByEmailAsync(email);
		if(user is null)
		{
			return new CommonResponse("user not found..!!", false);
		}

		if (user.LockoutEnd != null)
		{
			return new CommonResponse("user blocked..!!", false);
		}

		var generateToken = await _unitOfWork.UserManager.GeneratePasswordResetTokenAsync(user);
		var tokenInBytes = Encoding.UTF8.GetBytes(generateToken);
		var token = WebEncoders.Base64UrlEncode(tokenInBytes);
		string url = $"{_unitOfWork.Configuration.GetValue<string>("AppUrl")}/ResetPassword?email={email}&token={token}";

		var emailBody = _emailService.ResetPasswordEmailBody(url);
		var result = await _emailService.SendEmailAsync(email, "Reset Password Request", emailBody, true);

		if (!result.IsSuccessed)
		{
			return result;
		}

		return new CommonResponse("check sent link to your inbox to reset password", true);

	}

	public async Task<CommonResponse> LoginAsync(LoginDto model)
	{
		var user = await _unitOfWork.UserManager.FindByEmailAsync(model.Email);
		if(user is null)
		{
			return new CommonResponse("user name or password not valid..!!!", false);
		}

		if (user.LockoutEnd != null)
		{
			return new CommonResponse("user blocked..!!", false);
		}

		var tokenCookieId = Helper.GetFirstFiveCharsFromId(user.Id);
		var httpContext = _httpContextAccessor.HttpContext;

		var checkLoginToken = httpContext?.Request.Cookies.FirstOrDefault(TK => TK.Key == $"loginToken-{tokenCookieId}").Value;

		if(checkLoginToken is not null)
		{
			return new CommonResponse("user already logged in..!!", false);
		}

		if (!user.EmailConfirmed)
		{
			var verifiationCode = new VerificationCodeDto(user.Id);
			return new CommonResponse("email not confirmed..!!", false);
		}

		if(await _unitOfWork.UserManager.IsLockedOutAsync(user))
		{
			return new CommonResponse("user blocked..!!", false);
		}

		var IsAuthenticated = await _unitOfWork.UserManager.CheckPasswordAsync(user, model.Password);
		if (!IsAuthenticated)
		{
			return new CommonResponse("user name or password not valid..!!!", false);
		}

		var token = await _tokenService.CreateLoginTokenAsync(user);
		var added = _tokenService.SaveTokenInCookie(token, user.Id);
		if(added == 0)
		{
			return new CommonResponse("return user logged in but can't save token in cookie", false);
		}
		else
		{
			var expire = _tokenService.GetExpirationTimeOfToken(token);
			var tokenResponse = new TokenDto(token, expire);
			return new CommonResponse("user logged in success..!!", true, null!, tokenResponse);
		}
	}

	public async Task<CommonResponse> LogoutAsync(string email)
	{
		var user = await _unitOfWork.UserManager.FindByEmailAsync(email);
		if(user is null)
		{
			return new CommonResponse("user not found..!!", false);
		}

		var httpContext = _httpContextAccessor.HttpContext;

		var theId = Helper.GetFirstFiveCharsFromId(user.Id);
		httpContext?.Response.Cookies.Delete($"loginToken-{theId}");
		return new CommonResponse("user logged out success..!!", true);
	}

	public async Task<CommonResponse> RegisterAsync(RegisterDto model)
	{
		IdentityRole mainRole;
		if (model.IsInstructor)
		{
			mainRole = await _unitOfWork.RoleManager.FindByNameAsync("Instructor");
			if(mainRole is null)
			{
				return new CommonResponse("the role is not found..!!", false);
			}
		}
		else
		{
			mainRole = await _unitOfWork.RoleManager.FindByNameAsync("Student");
			if (mainRole is null)
			{
				return new CommonResponse("the role is not found..!!", false);
			}
		}

		return await _handlerService.RegisterHandlerAsync(model, mainRole.Name, mainRole.Name);
	}

	

	public async Task<CommonResponse> RemoveAccountAsync(RemoveAccountDto model)
	{
		var user = await _unitOfWork.UserManager.FindByEmailAsync(model.Email);
		if (user is null)
		{
			return new CommonResponse("user not found...!!!", false);
		}

		var IsAuthenticated = await _unitOfWork.UserManager.CheckPasswordAsync(user, model.Password);
		if (!IsAuthenticated)
		{
			return new CommonResponse("the password is not valid..!!", false);
		}
		

		//> delete it from related tables first
		if(await _unitOfWork.UserManager.IsInRoleAsync(user, "Student"))
		{
			var student = await _unitOfWork.StudentRepo.GetByRefIdAsync(user.Id);
			_unitOfWork.StudentRepo.Delete(student);
			_unitOfWork.StudentRepo.SaveChanges();
		}
		
		if(await _unitOfWork.UserManager.IsInRoleAsync(user, "Instructor"))
		{
			var instructor = await _unitOfWork.InstructorRepo.GetByRefIdAsync(user.Id);

			//> check if instructor has courses
			int coursesCount = _unitOfWork.CourseRepo.GetInstructorCoursesCount(instructor.Id);
			if(coursesCount > 0)
			{
				return new CommonResponse("your account have courses and may students enrolled into your courses, so we can't delete your account", false);
			}

			//> delete account if have no created courses
			await _unitOfWork.CourseRepo.DeleteInstrutorCourses(instructor.Id);

			_unitOfWork.InstructorRepo.Delete(instructor);
			_unitOfWork.InstructorRepo.SaveChanges();
		}

		await LogoutAsync(user.Email);

		var result = await _unitOfWork.UserManager.DeleteAsync(user);
		if (!result.Succeeded)
		{
			return new CommonResponse("cannot remove user right now, try again later..!!", false);
		}

		return new CommonResponse("user deleted bye bye ... 😘", true);
	}

	public async Task<CommonResponse> ResendConfirmationEmail(string email)
	{
		var user = await _unitOfWork.UserManager.FindByEmailAsync(email);
		if (user is null)
		{
			return new CommonResponse("user not found...!!!", false);
		}

		if (user.LockoutEnd != null)
		{
			return new CommonResponse("user blocked..!!", false);
		}

		string verificationCode = _emailService.GenerateOTPCode();
		string emailBody = _emailService.GetConfirmationEmailBody(verificationCode, user.DisplayName);

		var userClaims = await _unitOfWork.UserManager.GetClaimsAsync(user);
		var generateToken = _tokenService.CreateToken(userClaims.ToList(), DateTime.Now.AddMinutes(5));
		string token = new JwtSecurityTokenHandler().WriteToken(generateToken);

		//> create object contains token and code and save it in cache
		var verificationObject = new VerficationTokenDto
		{
			OtpCode = verificationCode,
			VerficationToken = token
		};

		string key = Helper.GetFirstFiveCharsFromId(user.Id);
		var expire = generateToken.ValidTo;

		await _redisService.SetDataAsync<VerficationTokenDto>(key, verificationObject, expire);

		var sended = await _emailService.SendEmailAsync(email, "Confirm Email", emailBody, true);
		if (!sended.IsSuccessed)
		{
			return sended;
		}

		return new CommonResponse("the email resended success..!!", true);
	}

	public async Task<CommonResponse> ResetPasswordAsync(ResetPasswordDto model, string token)
	{
		var user = await _unitOfWork.UserManager.FindByEmailAsync(model.Email);
		if(user is null)
		{
			return new CommonResponse("user not found...!!!", false);
		}

		if (user.LockoutEnd != null)
		{
			return new CommonResponse("user blocked..!!", false);
		}

		if (model.NewPassword != model.ConfirmPassword)
		{
			return new CommonResponse("passwords are not matches..!!", false);
		}

		var decodedToken = WebEncoders.Base64UrlDecode(token);
		var theToken = Encoding.UTF8.GetString(decodedToken);

		var result = await _unitOfWork.UserManager.ResetPasswordAsync(user, theToken, model.NewPassword);

		if (!result.Succeeded)
		{
			return new CommonResponse("cannot change password for now because the link is expired, order new one", false);
		}
		return new CommonResponse("password changes success", true);
	}

	public async Task<CommonResponse> UpdatePasswordAsync(UpdatePasswordDto model)
	{
		var user = await _unitOfWork.UserManager.FindByEmailAsync(model.email);
		if (user is null)
		{
			return new CommonResponse("user not found...!!!", false);
		}

		var isAuthenticated = await _unitOfWork.UserManager.CheckPasswordAsync(user, model.OldPassword);
		if (!isAuthenticated)
		{
			return new CommonResponse("old password invalid..!!", false);
		}

		if(model.NewPassword != model.ConfirmPassword)
		{
			return new CommonResponse("new passwords are not matched..!", false);
		}

		var result = await _unitOfWork.UserManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
		if(!result.Succeeded)
		{
			return new CommonResponse("cannot change password right now, try again later..!!", false);
		}

		return new CommonResponse("password change success..!!", true);
	}

	public async Task<CommonResponse> UpdateAccountInfoAsync(UpdateAccountInfoDto model, string email)
	{
		var user = await _unitOfWork.UserManager.FindByEmailAsync(email);
		if(user is null)
		{
			return new CommonResponse("user not found..!!", false);
		}

		if (!string.IsNullOrEmpty(model.DisplayName))
		{
			user.DisplayName = model.DisplayName;
		}

		if (!string.IsNullOrEmpty(model.UserName))
		{
			var isUserNameExist = await _unitOfWork.UserManager.FindByNameAsync(model.UserName);
			if(isUserNameExist is not null)
			{
				return new CommonResponse("UserName already taken", false);
			}

			user.UserName = model.UserName;
		}

		if (!string.IsNullOrEmpty(model.AvatarUrl))
		{
			user.Avatar = model.AvatarUrl;
		}

		if (user.Address is not null)
		{
			user.Address = model.Address;
		}


		var updated = await _unitOfWork.UserManager.UpdateAsync(user);
		if (!updated.Succeeded)
		{
			return new CommonResponse("cannot update info right now, try again later..!!", false);
		}

		return new CommonResponse("account updated success..!!", true);
	}

	public async Task<CommonResponse> MarkUserAsAdminAsync(string email)
	{
		var user = await _unitOfWork.UserManager.FindByEmailAsync(email);
		if(user is null)
		{
			return new CommonResponse("user not found..!", false);
		}

		if(await _unitOfWork.UserManager.IsInRoleAsync(user, "Admin"))
		{
			return new CommonResponse("user already have admin role..!!", false);
		}

		var claims = await _unitOfWork.UserManager.GetClaimsAsync(user);
		var currentRole = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

		//> remove current role
		var removeClaimResult = await _unitOfWork.UserManager.RemoveClaimAsync(user, currentRole);
		if (!removeClaimResult.Succeeded)
		{
			return new CommonResponse("cannot remove current role claim..!", false);
		}

		//> add new role(admin)
		var addClaimResult = await _unitOfWork.UserManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Admin"));
		if (!addClaimResult.Succeeded)
		{
			return new CommonResponse("cannot assign new role claim to user..!!", false);
		}

		var addToRoleResult = await _unitOfWork.UserManager.AddToRoleAsync(user, "Admin");
		if(!addToRoleResult.Succeeded)
		{
			return new CommonResponse("cannot assign admin role to user right now..!!", false);
		}

		var resultOfUpdate = await _unitOfWork.UserManager.UpdateAsync(user);
		if(!resultOfUpdate.Succeeded)
		{
			return new CommonResponse("cannot assign admin role to user right now..!!", false);
		}

		return new CommonResponse("Admin role assigned to the user..!!", true);

	}

	public async Task<CommonResponse> RegisterAdminAsync(RegisterDto model)
	{
		var mainRole = await _unitOfWork.RoleManager.FindByNameAsync("Admin");
		if(mainRole is null)
		{
			return new CommonResponse("cannot find the admin role, tell another admin to create it..!!", false);
		}

		return await _handlerService.RegisterHandlerAsync(model, mainRole.Name, mainRole.Name);
	}

	public async Task<GetUserDto> GetUserByEmailOrUserNameAsync(string emailOrUserName)
	{
		var user = await _unitOfWork.UserManager.FindByEmailAsync(emailOrUserName);
		if (user is null)
		{
			user = await _unitOfWork.UserManager.FindByNameAsync(emailOrUserName);
			if(user is null)
			{
				return null!;
			}
		}

		var claims = await _unitOfWork.UserManager.GetClaimsAsync(user);
		var role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

		int? coursesCreated = 0;
		int? coursesEnrolled = 0;

		if(await _unitOfWork.UserManager.IsInRoleAsync(user, "Instructor"))
		{
			var instructor = await _unitOfWork.InstructorRepo.GetByRefIdAsync(user.Id);
			if(instructor is not null)
			{
				coursesCreated = _unitOfWork.CourseRepo.GetInstructorCoursesCount(instructor.Id);
			}
		}

		if (await _unitOfWork.UserManager.IsInRoleAsync(user, "Student"))
		{
			var student = await _unitOfWork.StudentRepo.GetByRefIdAsync(user.Id);
			if (student is not null)
			{
				coursesEnrolled = _unitOfWork.StudentCourseRepo.GetStudentCoursesCount(student.Id);
			}
		}

		return new GetUserDto
		{
			Email = user.Email,
			UserName = user.UserName,
			Role = role ?? "NA",
			DisplayName = user.DisplayName,
			PhoneNumber = user.PhoneNumber,
			CoursesCreated = (int)coursesCreated!,
			CoursesEnrolled = (int)coursesEnrolled!
		};
	}


	public int GetUsersNumber()
	{
		return _unitOfWork.UserManager.Users.Count();
	}

	public async Task<int> GetStudentsNumberAsync()
	{
		var users = await _unitOfWork.UserManager.GetUsersInRoleAsync("Student");
		return users.Count();
 	}

	public async Task<int> GetInstructorsNumberAsync()
	{
		var users = await _unitOfWork.UserManager.GetUsersInRoleAsync("Instructor");
		return users.Count();
	}

	public async Task<int> GetAdminsCountAsync()
	{
		var users = await _unitOfWork.UserManager.GetUsersInRoleAsync("Admin");
		return users.Count();
	}

	public async Task<int> GetBockedUsersCountAsync()
	{
		var blockedUsers = await _unitOfWork.UserManager.Users.Where(user => user.LockoutEnd != null).ToListAsync();
		return blockedUsers.Count();
	}


	public async Task<CommonResponse> BlockUserForPeriodAsync(BlockUserDto model)
	{
		var user = await _unitOfWork.UserManager.FindByEmailAsync(model.Email);
		if(user is null)
		{
			return new CommonResponse("user not found..!!", false);
		}

		if (model.IsForever)
		{
			user.LockoutEnd = DateTime.Now.AddYears(1000);
			var resultOfBlockForever = await _unitOfWork.UserManager.UpdateAsync(user);
			if (!resultOfBlockForever.Succeeded)
			{
				return new CommonResponse("cannot block user right now..!!", false);
			}

			return new CommonResponse("user blocked forever..!!", true);
		}

		user.LockoutEnd = DateTime.Now.AddDays(model.NumberOfDays);
		var result = await _unitOfWork.UserManager.UpdateAsync(user);
		if (!result.Succeeded)
		{
			return new CommonResponse("cannot block user right now..!!", false);
		}

		return new CommonResponse($"user blocked for {model.NumberOfDays} days", true);
	}

	public async Task<CommonResponse> UnBlockUserForPeriodAsync(string email)
	{
		var user = await _unitOfWork.UserManager.FindByEmailAsync(email);
		if (user is null)
		{
			return new CommonResponse("user not found..!!", false);
		}

		user.LockoutEnd = null!;
		var result = await _unitOfWork.UserManager.UpdateAsync(user);
		if (!result.Succeeded)
		{
			return new CommonResponse("cannot block user right now..!!", false);
		}

		return new CommonResponse("User Unblocked..!!", true);
	}

	
}
