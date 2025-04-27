using FluentValidation;
using Server.CQRS.Post.Query;

namespace Server.CQRS.Post.Validations
{
    public class GetPostByIdValidation : AbstractValidator<GetPostsIdQuery>
    {
        public GetPostByIdValidation()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("PostId can't be empty");
        }
    }
}
