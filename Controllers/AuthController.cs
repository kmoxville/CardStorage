using CardStorage.Data.Requests.AuthRequests;
using CardStorage.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace CardStorage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);
            if (response.Status == Data.Responses.AuthStatuses.Success)
            {
                Response.Headers.Add("X-Session-Token", response.SessionToken);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(CreateRequest request)
        {
            var response = await _authService.CreateAsync(request);
            
            return Ok(response);
        }
    }
}
