using EMS.Model.Dtos.Login;

namespace EMS.Service.Users
{
	public interface IUserService
	{
		LoginResponseDto LoginUser(LoginDto loginRequest);
	}
}
