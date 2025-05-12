using MockApiIntegration.Application.DTOs;
using FluentValidation;

namespace MockApiIntegration.Application.Validators
{
    // Validator to ensure the integrity of incoming product creation data.
    //Name is mandatory, data cannot be null
    public class ProductRequestDtoValidator : AbstractValidator<ProductRequestDto>
    {
        public ProductRequestDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Data).NotNull();
        }
    }
}
