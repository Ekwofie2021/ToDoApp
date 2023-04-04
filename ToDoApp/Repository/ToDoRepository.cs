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
            foreach (var item in _toDoList)
            {
                if (toDo.Id == item.Id)
                {
                    item.IsCompleted = toDo.IsCompleted;
                    break;
                }
            }
        }
    }
}
