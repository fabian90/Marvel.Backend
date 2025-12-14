using FluentValidation;
using Marvel.Application.DTOs.Auth;

namespace Marvel.Application.Validators.Auth
{
    public class LoginRequestDtoValidator
        : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}