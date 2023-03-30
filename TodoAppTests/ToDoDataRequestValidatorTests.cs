using AutoFixture;
using FluentAssertions;
using ToDoApp.Models;
using ToDoApp.Validation;
using Xunit;

namespace TodoAppTests
{
  public class ToDoDataRequestValidatorTests
  {
    private readonly Fixture _fixture;
    private readonly ToDoDataRequestValidator _validator;
    public ToDoDataRequestValidatorTests()
    {
      _fixture = new Fixture();
      _validator = new ToDoDataRequestValidator();
    }

    [Fact]
    public void Title_ShouldHaveErrorWhenItsEmptyOrWhiteSpace()
    {
      //Arrange
      var titleErrorMsg = "Please provide title for your To-do";
      var model = _fixture.Build<ToDoDataRequest>().Without(x => x.Title).Create();

      //Act
      var result = _validator.Validate(model);

      //Assert
      result.Errors[0].ErrorMessage.Should().Be(titleErrorMsg);
      result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Description_ShouldHaveErrorWhenItsEmptyOrWhiteSpace()
    {
      //Arrange
      var descriptionErrorMsg = "Please provide description To-do";
      var model = _fixture.Build<ToDoDataRequest>().Without(x => x.Description).Create();

      //Act
      var result = _validator.Validate(model);

      //Assert      
      result.Errors[0].ErrorMessage.Should().Be(descriptionErrorMsg);
      result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Id_ShouldHaveErrorWhenItsEmptyOrWhiteSpace()
    {
      //Arrange
      var model = _fixture.Build<ToDoDataRequest>().Without(x => x.Description).Create();

      //Act
      var result = _validator.Validate(model);

      //Assert
      result.IsValid.Should().BeFalse();
    }
  }
}