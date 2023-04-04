namespace ToDoApp.Models
{
    public class ToDoDto : IToDo
  {
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty; 
    public Guid Id { get; set; }
    public bool IsCompleted { get; set; }
  }
}
