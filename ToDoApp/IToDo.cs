namespace ToDoApp
{
    public interface IToDo
    {
        public Guid Id { get; set; }
        string Description { get; set; }
        bool IsCompleted { get; set; }
        string Title { get; set; }
    }
}