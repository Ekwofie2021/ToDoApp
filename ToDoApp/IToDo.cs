namespace ToDoApp
{
    public interface IToDo
    {
        public Guid Id { get; set; }
        string Description { get; set; }
        Action Status { get; set; }
        string Title { get; set; }
    }
}