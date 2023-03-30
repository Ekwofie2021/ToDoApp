using ToDoApp.Repository;

namespace ToDoApp.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _toDoRepository;

        public ToDoService(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public IEnumerable<IToDo> GetAllToDo()
        {
            return _toDoRepository.GetAllToDo();
        }

        public IToDo? GetToDoById(Guid id)
        {
            return _toDoRepository.GetToDoById(id);
        }

        public void SaveToDo(IToDo toDo)
        {
            _toDoRepository.SaveToDo(toDo);
        }

        public void UpdateToDoStatus(IToDo toDo)
        {
            _toDoRepository.UpdateToDoStatus(toDo);
        }
    }
}
