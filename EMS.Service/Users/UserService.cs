using EMS.Model.Dtos.Login;
namespace EMS.Service.Users
{
	public class UserService : IUserService
	{
		public LoginResponseDto LoginUser(LoginDto loginRequest)
		{
			LoginResponseDto loginResponse = new();
			if (loginRequest.Username.Equals("admin") && loginRequest.Password.Equals("P@ssw0rd"))
			{
				loginResponse.UserId = 1;
				loginResponse.Email = "im.abhanan@gmail.com";
				loginResponse.Username = loginRequest.Username;
			}
			return loginResponse;
		}
	}
}
