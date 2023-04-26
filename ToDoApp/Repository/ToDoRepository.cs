namespace ToDoApp.Repository
{
  public sealed class ToDoRepository : IToDoRepository
  {
    private readonly List<IToDo> _toDoList;
    public ToDoRepository()
    {
      _toDoList = new List<IToDo>();
    }

    public IEnumerable<IToDo> GetAllToDo()
    {
      return _toDoList;
    }

    public IToDo? GetToDoById(Guid id)
    {
      return _toDoList.FirstOrDefault(t => t.Id == id);
    }

    public void SaveToDo(IToDo toDo)
    {
      _toDoList.Add(toDo);
    }

    public void UpdateToDoStatus(IToDo toDo)
    {
      var result = _toDoList.FirstOrDefault(x => x.Id == toDo.Id);
      if (result != null)
      {
        result.IsCompleted = toDo.IsCompleted;
      }
    }
  }
}
