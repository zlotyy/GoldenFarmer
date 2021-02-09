using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zlotyy.GoldenFarmer.TransportModels.Users;
using Zlotyy.GoldenFarmer.Web.Services;
using Zlotyy.GoldenFarmer.Web.Utils;
using UserCredentialsModel = Zlotyy.GoldenFarmer.TransportModels.Users.UserCredentials;

namespace Zlotyy.GoldenFarmer.Web.Controllers
{
    public class LoginController : BaseController
    {
        private string ApiUrl(string endpoint = "") => $"/api/v1/user/{endpoint}";
        private readonly IJwtAuthenticationManager _authManager;        
        private readonly IRestService _restService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IJwtAuthenticationManager authManager, IRestService restService, ILogger<LoginController> logger)
        {
            _authManager = authManager;
            _restService = restService;
            _logger = logger;
        }        

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCredentialsModel model)
        {
            var userResult = _restService.Post<UserModel>(ApiUrl(), model);
            if (!userResult.IsSuccessful)
            {
                return BadRequest();
            }

            var user = userResult.Data;
            if (user == null)
            {
                return Unauthorized();
            }
                        
            var userId = user.UserId;
            var token = _authManager.CreateToken(userId.ToString());
            return Ok(token);
        }
    }
}
