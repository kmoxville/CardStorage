using CardStorage.Data.Requests.AuthRequests;
using FluentValidation;

namespace CardStorage.Services.Validation.Requests
{
    public interface ICreateUserRequestValidator : IValidationService<CreateRequest>
    {

    }

    public sealed class CreateUserRequestValidator : ValidationService<CreateRequest>, ICreateUserRequestValidator
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.UserName)
                .EmailAddress()
                .WithErrorCode("RFS-103.1");

            RuleFor(x => x.Password)
                .MinimumLength(10)
                .WithErrorCode("RFS-103.2");
        }
    }
}
