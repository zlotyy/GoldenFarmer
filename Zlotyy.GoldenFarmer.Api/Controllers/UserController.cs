using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zlotyy.GoldenFarmer.Api.Queries;
using Zlotyy.GoldenFarmer.TransportModels.Users;

namespace Zlotyy.GoldenFarmer.Api.Controllers
{
    public class UserController : BaseSecureController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserQueries _queries;

        public UserController(ILogger<UserController> logger, IUserQueries queries)
        {
            _logger = logger;
            _queries = queries;
        }

        [HttpPost]
        public UserModel GetUser([FromBody] UserCredentials credentials)
        {
            _logger.LogDebug($"Request {nameof(GetUser)}, login: {credentials.Login}");
            var user = _queries.GetUser(credentials);
            var userModel = user != null ? new UserModel(user) : null;
            return userModel;
        }
    }
}
