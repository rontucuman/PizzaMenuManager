using FluentValidation;
namespace PizzaMenu.Domain.Requests.Pizza.Validators
{
  public class AddPizzaRequestValidator : AbstractValidator<AddPizzaRequest>
  {
    public AddPizzaRequestValidator()
    {
      RuleFor( x => x.Name ).NotEmpty();
    }
  }
}
