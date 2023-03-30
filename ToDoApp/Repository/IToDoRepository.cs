namespace ToDoApp.Repository
{
    public interface IToDoRepository
    {
        IToDo? GetToDoById(Guid id);
        IEnumerable<IToDo> GetAllToDo();
        void UpdateToDoStatus(IToDo toDo);
        void SaveToDo(IToDo toDo);
    }
}
