

namespace learningManagementSystem.BLL.Service;

public class HandlerService : IHandlerService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IEmailService _emailService;
	private readonly ITokenService _tokenService;
	private readonly IRedisService _redisService;

	public HandlerService(IUnitOfWork unitOfWork, IEmailService emailService, ITokenService tokenService, IRedisService redisService)
    {
		_unitOfWork = unitOfWork;
		_emailService = emailService;
		_tokenService = tokenService;
		_redisService = redisService;
	}
    public async Task<CommonResponse> RegisterHandlerAsync(RegisterDto model, string mainRole, params string[] otherRoles)
	{
		var checkEmailIsExist = await _unitOfWork.UserManager.FindByEmailAsync(model.Email);
		if(checkEmailIsExist is not null)
		{
			return new CommonResponse("email already taken..!!", false);
		}

		var checkUserNameIsExist = await _unitOfWork.UserManager.FindByNameAsync(model.UserName);
		if( checkUserNameIsExist is not null)
		{
			return new CommonResponse("UserName already taken..!!", false);
		}

		ApplicationUser newUser = new ApplicationUser()
		{
			DisplayName = model.DisplayName,
			Email = model.Email,
			UserName = model.UserName,
		};

		var userClaims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, newUser.Id),
			new Claim(ClaimTypes.Email, newUser.Email),
			new Claim(ClaimTypes.Role, mainRole),
		};

		string OTP = _emailService.GenerateOTPCode();
		string emailBody = _emailService.GetConfirmationEmailBody(OTP, newUser.DisplayName);


		var verifcationAccountTokenClaims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, newUser.Id),
			new Claim(ClaimTypes.Email, newUser.Email),
			new Claim(ClaimTypes.Role, mainRole),
			new Claim("VerficationCode", OTP),
		};

		var CreateVerifcationAccountToken = _tokenService.CreateToken(verifcationAccountTokenClaims, DateTime.Now.AddMinutes(5));
		string verifcationAccountToken = new JwtSecurityTokenHandler().WriteToken(CreateVerifcationAccountToken);

		//> cache the token and 
		var verifcationAccountTokenObject = new VerficationTokenDto
		{
			OtpCode = OTP,
			VerficationToken = verifcationAccountToken,
		};

		string key = Helper.GetFirstFiveCharsFromId(newUser.Id);
		var expire = CreateVerifcationAccountToken.ValidTo;

		await _redisService.SetDataAsync<VerficationTokenDto>(key, verifcationAccountTokenObject, expire);

		var resultOfCreate = await _unitOfWork.UserManager.CreateAsync(newUser, model.Password);
		if (!resultOfCreate.Succeeded)
		{
			return new CommonResponse("cannot register you right now, try again..!!", false, Helper.GetErrorsOfIdentityResult(resultOfCreate.Errors));
		}

		//> add the claims to claims table in Db & Add roles for the User
		await _unitOfWork.UserManager.AddClaimsAsync(newUser, userClaims);
		await _unitOfWork.UserManager.AddToRolesAsync(newUser, otherRoles);

		//> check user registered as student or instructor and assign it in right table
		await RegisterUserAsInsturctorOrStudent(newUser, model.IsInstructor);

		var sended = await _emailService.SendEmailAsync(newUser.Email, "Confirm Email", emailBody, true);
		if (!sended.IsSuccessed)
		{
			return sended;
		}

		var verificationModel = new VerificationCodeDto(newUser.Id);
		return new CommonResponse("check your inbox to confrim the email", true, null!, verificationModel);
	}


	private async Task RegisterUserAsInsturctorOrStudent(ApplicationUser user, bool isInstructor)
	{
		if (isInstructor)
		{
			Instructor newInstructor = new()
			{
				Id = Guid.NewGuid(),
				UserRefId = user.Id,
				AppUser = user,
			};

			await _unitOfWork.InstructorRepo.CreateAsync(newInstructor);
			await _unitOfWork.InstructorRepo.SaveChangesAsync();
		}
		else
		{
			Student newInstructor = new()
			{
				Id = Guid.NewGuid(),
				UserRefId = user.Id,
				AppUser = user,
			};

			await _unitOfWork.StudentRepo.CreateAsync(newInstructor);
			await _unitOfWork.StudentRepo.SaveChangesAsync();
		}
	}
}
