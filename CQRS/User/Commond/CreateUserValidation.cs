using FluentValidation;

namespace Server.CQRS.User.Commond
{
    public class CreateUserValidation:AbstractValidator<CreateUserCommond>
    {
        public CreateUserValidation() 
        {

            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Username Can't be Empty!")
                .MaximumLength(50);

            RuleFor(x => x.Email)
        .NotEmpty().WithMessage("Email is required.")
        .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
        }


    }
}
