using AutoFixture;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDoApp.Controllers;
using ToDoApp.Models;
using ToDoApp.Services;
using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Xunit;

namespace TodoAppTests
{
  public class ToDosControllerTests
  {
    private readonly Mock<IToDoService> _toDoService;
    private readonly ToDosController _controller;
    private readonly Fixture _fixture;
    private readonly Mock<IObjectModelValidator> _objectValidator;

    public ToDosControllerTests()
    {
      _fixture = new Fixture();
      _toDoService = new Mock<IToDoService>();
      _controller = new ToDosController(_toDoService.Object);

      _objectValidator = new Mock<IObjectModelValidator>();
      _objectValidator.Setup(o => o.Validate(It.IsAny<ActionContext>(),
                                        It.IsAny<ValidationStateDictionary>(),
                                        It.IsAny<string>(),
                                        It.IsAny<Object>()));
    }

    [Fact]
    public void GetAllToDo_WhenUserRequestForAllTheToDoList_ShouldNotReturn5ToDoDtos()
    {
      //Arrange
      int toDoCount = 5;
      _toDoService.Setup(r => r.GetAllToDo()).Returns(_fixture.CreateMany<ToDoDto>(toDoCount));

      //Act
      var actionResult = _controller.GetAllTodo();

      var result = actionResult as OkObjectResult;
      var todos = result?.Value as IEnumerable<ToDoDto>;

      //Assert
      todos?.Count().Should().Be(toDoCount);
    }

    [Theory]
    [AutoData]
    public void GetTodoById_WhenUserRequestToGetToDoById_ShouldReturn1MatchingToDoDto(Guid toDoId)
    {
      //Arrange
      var mockToDo = _fixture.Build<ToDoDto>().With(x => x.Id, toDoId).Create();
      _toDoService.Setup(r => r.GetToDoById(toDoId)).Returns(mockToDo);

      // Act
      var actionResult = _controller.GetTodoById(toDoId);

      var result = actionResult as OkObjectResult;
      var expectToDo = result?.Value as ToDoDto;

      //Assert
      expectToDo?.Id.Should().Be(toDoId);
    }

    [Fact]
    public void GetTodoById_WhenUserRequestGetToDoByIdWithUnMatchId_ShouldReturnNotFoundResult()
    {
      //Arrange
      _toDoService.Setup(r => r.GetToDoById(Guid.NewGuid())).Returns(_fixture.Create<ToDoDto>());

      // Act
      var actionResult = _controller.GetTodoById(Guid.NewGuid());

      //Assert
      actionResult.Should().BeOfType<NotFoundResult>(); ;
    }

    [AutoData]
    [Theory]
    public void CreateTodo_WhenUserRequestToAddNewToDo_ShouldCreateNewToDo(ToDoDataRequest toDoDataRequest)
    {
      //Arrange

      toDoDataRequest.Description = string.Empty;

      _toDoService.Setup(r => r.SaveToDo(toDoDataRequest));

      // Act
      var actionResult = _controller.CreateTodo(toDoDataRequest);

      var result = actionResult as CreatedAtRouteResult;
      var expectToDo = result?.Value as ToDoDataRequest;

      //Assert
      _toDoService.Verify(x => x.SaveToDo(It.IsAny<ToDoDataRequest>()), Times.Once);
      expectToDo?.Id.Should().Be(toDoDataRequest.Id);
    }

    [AutoData]
    [Theory]
    public void UpdateToDoStatus_WhenUserRequestToUpDateToDiFieldWithMatchId_ShouldUpdateAndReturnNoContentResult(Guid toDoId)
    {
      //Arrange
      var patch = new JsonPatchDocument<ToDoDataRequest>();
      patch.Replace(x => x.Title, "test");

      var mockToDoDto = _fixture.Build<ToDoDto>().With(x => x.Id, toDoId).Create();
      var mockTodoRequest = _fixture.Build<ToDoDataRequest>().With(x => x.Id, toDoId).Create();

      _toDoService.Setup(r => r.GetToDoById(toDoId)).Returns(mockToDoDto);
      _toDoService.Setup(r => r.UpdateToDoStatus(mockTodoRequest));
      _controller.ObjectValidator = _objectValidator.Object;

      // Act
      var actionResult = _controller.UpdateToDoStatus(mockTodoRequest.Id, patch);

      //Assert
      actionResult.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public void UpdateToDoStatus_WhenUserRequestToUpDateToDoFieldWithIncorrectId_ShouldUpdateAndReturnNotFoundResultt()
    {
      //Arrange
      var patch = new JsonPatchDocument<ToDoDataRequest>();
      patch.Replace(x => x.Title, "test");

      var mockToDoDto = _fixture.Create<ToDoDto>();
      var mockTodoRequest = _fixture.Build<ToDoDataRequest>().With(x => x.Id, Guid.NewGuid).Create();

      _toDoService.Setup(r => r.GetToDoById(mockToDoDto.Id)).Returns(mockToDoDto);
      _toDoService.Setup(r => r.UpdateToDoStatus(mockTodoRequest));

      // Act
      var actionResult = _controller.UpdateToDoStatus(mockTodoRequest.Id, patch);

      //Assert
      actionResult.Should().BeOfType<NotFoundResult>();
    }

    [AutoData]
    [Theory]
    public void UpdateToDoStatus_WhenUserRequestToUpDateToDoFieldWithCorrectId_ShouldUpdateAndReturnBadRequestResult(Guid toDoId)
    {
      //Arrange
      var patch = new JsonPatchDocument<ToDoDataRequest>();
      patch.Replace(x => x.Title, "testing");

      var mockToDoDto = _fixture.Build<ToDoDto>().With(x => x.Id, toDoId).Create();
      var mockTodoRequest = _fixture.Build<ToDoDataRequest>().With(x => x.Id, toDoId).Create();

      _toDoService.Setup(r => r.GetToDoById(toDoId)).Returns(mockToDoDto);
      _toDoService.Setup(r => r.UpdateToDoStatus(mockTodoRequest));
      _controller.ModelState.AddModelError("error", "error"); 

      // Act
      var actionResult = _controller.UpdateToDoStatus(mockTodoRequest.Id, patch);

      //Assert
      actionResult.Should().BeOfType<BadRequestResult>();
    }
  }
}
