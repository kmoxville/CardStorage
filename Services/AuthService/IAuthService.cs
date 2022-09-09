using CardStorage.Data.Requests.AuthRequests;
using CardStorage.Data.Responses.AuthResponses;

namespace CardStorage.Services.AuthService
{
    public interface IAuthService
    {
        public Task<LoginResponse> LoginAsync(LoginRequest request);

        public Task<CreateResponse> CreateAsync(CreateRequest request);
    }
}
