using FluentValidation;

namespace BusinessLogic.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(salary => salary.ProductId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty.");

            RuleFor(salary => salary.Status)
                .InclusiveBetween(0, 1)
                .WithMessage("{PropertyName} must be 0 or 1");

            RuleFor(salary => salary.Stock)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(salary => salary.Price)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
