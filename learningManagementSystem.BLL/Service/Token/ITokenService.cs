

namespace learningManagementSystem.BLL.Service;

public interface ITokenService
{
	JwtSecurityToken CreateToken(List<Claim> claims, DateTime expireationTime);
	SigningCredentials GetCredentials();
	SymmetricSecurityKey GetKey();
	string GetKeyAsString();
	JwtSecurityToken ReadToken(string token);
	string ExtractClaimFromToken(string token, string tokenClaim);
	DateTime GetExpirationTimeOfToken(string token);
	bool IsTokenExpired(string token);
	Task<string> CreateLoginTokenAsync(ApplicationUser user);
	int SaveTokenInCookie(string token, string id);
}
