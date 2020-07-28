using FluentValidation;

namespace PizzaMenu.Domain.Requests.Ingredient.Validators
{
  public class AddIngredientRequestValidator : AbstractValidator<AddIngredientRequest>
  {
    public AddIngredientRequestValidator()
    {
      RuleFor( ingredient => ingredient.Name ).NotEmpty();
    }
  }
}
