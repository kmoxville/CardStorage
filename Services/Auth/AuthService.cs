using CardStorage.Data.Entities;
using CardStorage.Data.Requests.AuthRequests;
using CardStorage.Data.Responses.AuthResponses;
using CardStorage.Data.UnitOfWork;
using CardStorage.Services.AuthService;
using CardStorage.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CardStorage.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public AuthService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public static string SecretKey = "testytrfhtfhgfhgfuyfut78yt87g87gh98h98y987tgyugfyug";

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var scope = _scopeFactory.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var response = new LoginResponse() { Status = Data.Responses.AuthStatuses.Fail, Success = false };

            var account = unitOfWork.Accounts.GetAll()
                .FirstOrDefault(x => x.Email == request.UserName);

            if (account != null && Password.VerifyPassword(request.Password, account.PasswordHash, account.PasswordSalt))
            {
                response.Status = Data.Responses.AuthStatuses.Success;
                response.Success = true;
            }
            else
            {
                return response;
            }

            var session = new Session()
            {
                Account = account,
                Token = CreateSessionToken(account)
            };

            await unitOfWork.Sessions.InsertAsync(session);
            await unitOfWork.SaveAsync();

            response.SessionToken = session.Token;

            return response;
        }

        private string CreateSessionToken(Account account)
        {
            JwtSecurityTokenHandler tokenHadler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(SecretKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, account.Email)
                    }),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHadler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHadler.WriteToken(token);
        }

        public async Task<CreateResponse> CreateAsync(CreateRequest request)
        {
            var scope = _scopeFactory.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var response = new CreateResponse() { Success = false };

            var alreadyExists = unitOfWork.Accounts.GetAll().Any(x => x.Email == request.UserName);
            if (alreadyExists)
            {
                response.ErrorMessage = "Account with this name already exists";
                return response;
            }

            Password password = new Password(request.Password);

            Account newAccount = new Account()
            {
                Email = request.UserName,
                PasswordSalt = password.Salt,
                PasswordHash = password.Hash
            };

            try
            {
                await unitOfWork.Accounts.InsertAsync(newAccount);
                await unitOfWork.Accounts.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                return response;
            }

            response.Success = true;
            return response;
        }
    }
}
