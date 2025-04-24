using FluentValidation;
using Server.CQRS.User.Register.Commond;

namespace Server.CQRS.User.Login.Commond
{
    public class CreateLoginValidation: AbstractValidator<CreateLoginCommond>
    {

        public CreateLoginValidation()
        {

            RuleFor(x => x.Email)
                  .NotEmpty().WithMessage("Email is required.");


            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");


        }
    }
}
