using FluentValidation.TestHelper;
using PizzaMenu.Domain.Requests.Pizza;
using PizzaMenu.Domain.Requests.Pizza.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PizzaMenu.Domain.Tests.Requests.Pizza.Validators
{
  public class AddPizzaRequestValidatorTests
  {
    private readonly AddPizzaRequestValidator _validator;

    public AddPizzaRequestValidatorTests()
    {
      _validator = new AddPizzaRequestValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
      AddPizzaRequest addPizzaRequest = new AddPizzaRequest { Name = string.Empty };
      _validator.ShouldHaveValidationErrorFor( x => x.Name, addPizzaRequest );
    }
  }
}
