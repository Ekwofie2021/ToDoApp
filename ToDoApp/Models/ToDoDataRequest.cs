namespace ToDoApp.Models
{
    public class ToDoDataRequest : IToDo
  {
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty; 
    public Action Status { get; set; }
    public Guid Id { get; set; }
  }
}
