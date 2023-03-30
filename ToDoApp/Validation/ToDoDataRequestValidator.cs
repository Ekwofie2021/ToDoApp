using FluentValidation;
using ToDoApp.Models;

namespace ToDoApp.Validation
{
  public class ToDoDataRequestValidator : AbstractValidator<ToDoDataRequest>
  {
    public ToDoDataRequestValidator() 
    {
      this.RuleFor(x => x.Id).NotEmpty();
      this.RuleFor(x => x.Title).NotEmpty().WithMessage("Please provide title for your To-do");
      this.RuleFor(x => x.Description).NotEmpty().WithMessage("Please provide description To-do");
    }
  }
}
