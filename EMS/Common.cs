using EMS.Model.Dtos.Login;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EMS
{
	public class Common
	{
		private readonly IConfiguration _configuration;
		public Common(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public string CreateToken(LoginResponseDto loginResponse, DateTime expireTime)
		{
			var claims = new[]
				{
			new Claim(JwtRegisteredClaimNames.NameId, loginResponse.UserId.ToString()),
			new Claim(JwtRegisteredClaimNames.UniqueName, loginResponse.Username),
			new Claim(JwtRegisteredClaimNames.UniqueName, loginResponse.Email),
			new Claim(JwtRegisteredClaimNames.Email, loginResponse.Email),
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var Issuer = _configuration["Jwt:Issuer"] + "";
			DateTime expiry = expireTime;
			var tokenString = new JwtSecurityToken(Issuer,
		   Issuer,
		   claims: claims,
		   expires: expiry,
		   signingCredentials: creds);
			var tokenStr = new JwtSecurityTokenHandler().WriteToken(tokenString);

			return tokenStr;
		}
	}
}
