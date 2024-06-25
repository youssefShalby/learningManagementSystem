

namespace learningManagementSystem.BLL.Service;
public class TokenService : ITokenService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public TokenService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
		_unitOfWork = unitOfWork;
		_httpContextAccessor = httpContextAccessor;
	}

    public async Task<string> CreateLoginTokenAsync(ApplicationUser user)
	{
		var userClaims = await _unitOfWork.UserManager.GetClaimsAsync(user);
		if(userClaims is null)
		{
			return "NA";
		}

		userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
		int numberOfDays = int.Parse(_unitOfWork.Configuration["JWT:TokenExpirePerDay"]);
		var generateToken = CreateToken(userClaims.ToList(), DateTime.Now.AddDays(numberOfDays));
		return new JwtSecurityTokenHandler().WriteToken(generateToken);
	}

	public JwtSecurityToken CreateToken(List<Claim> claims, DateTime expireationTime)
	{
		return new JwtSecurityToken(
			issuer: _unitOfWork.Configuration["JWT:issuer"],
			audience: _unitOfWork.Configuration["JWT:audience"],
			claims: claims,
			notBefore: DateTime.Now,
			expires: expireationTime,
			signingCredentials: GetCredentials()
			);
	}

	public string ExtractClaimFromToken(string token, string tokenClaim)
	{
		var theToken = ReadToken(token);
		return theToken.Claims.FirstOrDefault(C => C.Type == tokenClaim)!.Value ?? "NA";
	}

	public SigningCredentials GetCredentials()
	{
		return new SigningCredentials(GetKey(), SecurityAlgorithms.HmacSha256Signature);
	}

	public DateTime GetExpirationTimeOfToken(string token)
	{
		return ReadToken(token).ValidTo;
	}

	public SymmetricSecurityKey GetKey()
	{
		var theKey = GetKeyAsString();
		var keyInBytes = Encoding.UTF8.GetBytes(theKey);
		return new SymmetricSecurityKey(keyInBytes);
	}

	public string GetKeyAsString()
	{
		return _unitOfWork.Configuration["JWT:tokenKey"];
	}

	public bool IsTokenExpired(string token)
	{
		return DateTime.UtcNow >= ReadToken(token).ValidTo;
	}

	public JwtSecurityToken ReadToken(string token)
	{
		JwtSecurityTokenHandler handler = new();
		var theToken = handler.ReadToken(token) as JwtSecurityToken;

		if (theToken is null) return null!;
		return theToken;
	}

	public int SaveTokenInCookie(string token, string id)
	{
		var httpContext = _httpContextAccessor.HttpContext;
		if(httpContext is null) return 0;

		var cookieOption = new CookieOptions
		{
			Expires = GetExpirationTimeOfToken(token)
		};

		string theId = Helper.GetFirstFiveCharsFromId(id);
		httpContext.Response.Cookies.Append($"loginToken-{theId}", token, cookieOption);
		return 1;
	}
}
