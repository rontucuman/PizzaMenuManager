using FluentValidation;

namespace PizzaMenu.Domain.Requests.Pizza.Validators
{
  public class EditPizzaRequestValidator : AbstractValidator<EditPizzaRequest>
  {
    public EditPizzaRequestValidator()
    {
      RuleFor(x => x.Guid).NotEmpty();
      RuleFor(x => x.Name).NotEmpty();
    }
  }
}
