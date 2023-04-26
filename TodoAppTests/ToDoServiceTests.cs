
using AutoFixture;
using Moq;
using ToDoApp.Models;
using ToDoApp.Repository;
using ToDoApp.Services;
using Xunit;
using FluentAssertions;
using AutoFixture.Xunit2;

namespace TodoAppTests
{
  public class ToDoServiceTests
  {
    private readonly Mock<IToDoRepository> _toDoRepository;
    private readonly ToDoService _toDoService;
    private readonly Fixture _fixture;
    public ToDoServiceTests()
    {
      _fixture = new Fixture();
      _toDoRepository = new Mock<IToDoRepository>();
      _toDoService = new ToDoService(_toDoRepository.Object);
    }

    [Fact]
    public void GetAllToDos_WhenUserRequestAllTodos_ShouldReturnAllTodos()
    {
      var toDoCount = 5;
      _toDoRepository.Setup(r => r.GetAllToDo()).Returns(_fixture.CreateMany<ToDoDto>(toDoCount));

      var result = _toDoService.GetAllToDo();
      
      result.Count().Should().Be(toDoCount);
    }

    [Theory]
    [AutoData]
    public void GetToDoById_WhenUserRequestToDoWithMatchingId_ShouldReturnTodo(Guid toDoId)
    {
      var mockToDo = _fixture.Build<ToDoDto>().With(x => x.Id, toDoId).Create();
      _toDoRepository.Setup(r => r.GetToDoById(toDoId)).Returns(mockToDo);

      var result = _toDoService.GetToDoById(toDoId);

      result?.Id.Should().Be(toDoId);
    }

    [Fact]
    public void GetToDoById_WhenUserRequestToDoWithUnMatchingId_ShouldReturnTodoNull()
    {
      var toDoId = Guid.NewGuid();
      _toDoRepository.Setup(r => r.GetToDoById(toDoId)).Returns(_fixture.Create<ToDoDto>());

      var result = _toDoService.GetToDoById(Guid.NewGuid());

      result.Should().BeNull();
    }

    [Fact]
    public void SaveTodo_WhenUserSendNewToDo_ShouldSaveNewToDo()
    {
      var toDo = _fixture.Create<ToDoDto>();

      _toDoService.SaveToDo(toDo);
      _toDoRepository.Verify(r => r.SaveToDo(It.IsAny<ToDoDto>()), Times.Once);
    }

    [Theory]
    [AutoData]
    public void UpdateToDoStatus_WhenUserUpdateToDoStatusWithCorrectId_ShouldNotUpdate(Guid toDoId)
    {
      var toDo = _fixture.Build<ToDoDto>()
        .With(x => x.Id, toDoId)
        .With(x => x.IsCompleted, false)
        .Create();

      _toDoRepository.Setup(r => r.GetToDoById(toDoId)).Returns(toDo);

      var toDoUpdate = _fixture.Build<ToDoDataRequest>()
        .With(x => x.Id, toDoId)
        .With(x => x.IsCompleted, true)
        .Create();

      _toDoRepository.Setup(r => r.UpdateToDoStatus(toDoUpdate));

      _toDoService.UpdateToDoStatus(toDoUpdate);

      var result = _toDoService.GetToDoById(toDoId);

      result?.IsCompleted.Should().NotBe(toDoUpdate.IsCompleted);
    }
  }
}