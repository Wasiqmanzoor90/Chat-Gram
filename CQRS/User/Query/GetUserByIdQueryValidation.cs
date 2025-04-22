using FluentValidation;

namespace Server.CQRS.User.Query
{
    public class GetUserByIdQueryValidation : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidation()
        {
            RuleFor(u => u.UserId)
                .NotEmpty().WithMessage("UserId must not be empty.");
        }
    }
}
