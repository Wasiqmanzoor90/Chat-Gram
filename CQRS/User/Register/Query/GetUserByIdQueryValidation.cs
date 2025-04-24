using FluentValidation;

namespace Server.CQRS.User.Register.Query
{
    public class GetUserByIdQueryValidation : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidation()
        {
            RuleFor(u => u.Id)
                .NotEmpty().WithMessage("UserId must not be empty.");
        }
    }
}
