using EMS.Model.Dtos.Login;
using EMS.Service.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace EMS.Controllers
{
	[Produces("application/json")]
	[Route("api/token")]
	public class TokenController : Controller
	{
		private readonly IUserService _userService;
		private readonly IConfiguration _configuration;

		public TokenController(IUserService userService, IConfiguration configuration)
		{
			_userService = userService;
			_configuration = configuration;
		}

		[AllowAnonymous]
		[HttpPost]
		[ActionName("create")]
		[Route("create")]
		public IActionResult Create([FromBody] LoginDto request)
		{
			if (!ModelState.IsValid)
				return BadRequest("Token failed to generate");

			var loggedInUser = _userService.LoginUser(request);
			if (loggedInUser == null || loggedInUser.UserId.Equals(0))
				return Ok(new { responseMessage = "Please enter Valid Username and Password." });

			DateTime expiryTime = DateTime.Now.AddMinutes(60);
			string jwtToken = new Common(_configuration).CreateToken(loggedInUser, expiryTime);

			return Ok(new
			{
				AccessToken = jwtToken,
				Expiry = expiryTime,
				Response = loggedInUser,
			});
		}
	}
}
