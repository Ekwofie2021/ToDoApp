using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
  [ApiController]
  [Route("api/todos")]
  public class ToDosController : Controller
  {
    private readonly IToDoService _toDoService;
    public ToDosController(IToDoService toDoService)
    {
      _toDoService = toDoService;
    }

    [HttpGet]
    public IActionResult GetAllTodo()
    {
      return Ok(_toDoService.GetAllToDo());
    }

    [HttpGet("{id}", Name = "GetToDoById")]
    public IActionResult GetTodoById(Guid id)
    {
      var result = _toDoService.GetToDoById(id);

      if (result == null)
      {
        return NotFound();
      }

      return Ok(_toDoService.GetToDoById(id));
    }

    [HttpPost]
    public IActionResult CreateTodo(ToDoDataRequest toDoDataRequest)
    {
      _toDoService.SaveToDo(toDoDataRequest);

      return CreatedAtRoute("GetTodoById", new { toDoDataRequest.Id }, toDoDataRequest);
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateToDoStatus(Guid id, JsonPatchDocument<ToDoDataRequest> patchDocument)
    {
      var todo = _toDoService.GetToDoById(id);
      if (todo == null)
      {
        return NotFound();
      }

      var updateTodo = new ToDoDataRequest
      {
        Id = todo.Id,
        IsCompleted = todo.IsCompleted,
        Description = todo.Description,
        Title = todo.Title
      };

      patchDocument.ApplyTo(updateTodo, ModelState);

      if (!ModelState.IsValid)
      {
        return BadRequest();
      }

      if (!TryValidateModel(updateTodo))
      {
        return BadRequest(ModelState);
      }

      todo.IsCompleted = updateTodo.IsCompleted;

      return NoContent();
    }
  }
}
