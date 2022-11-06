namespace IaaS2;

public interface IToDoRepository
{

    ToDo GetToDoById(int id);
    IEnumerable<ToDo> GetAllToDos();
    void CreateToDo(ToDo toDo);

    void DeleteToDo(ToDo toDo);
}

    public class ToDoRepository : IToDoRepository
    {

        private static int NextId = 6;
        private static List<ToDo> ToDos = new List<ToDo> 
        {
              new ToDo {TaskDescription = "REST APIs verstehen", Id=1},
              new ToDo {TaskDescription = "API lokal testen", Id=2},
              new ToDo {TaskDescription = "Virtuelle Maschine in AZURE erstellen", Id=3},
              new ToDo {TaskDescription = "API auf VM installieren", Id=4},
              new ToDo {TaskDescription = "API in AZURE testen", Id=5}
        };

        public ToDoRepository()
        {

        }


        public ToDo GetToDoById(int id)
        {
            return ToDos.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<ToDo> GetAllToDos()
        {
            return  ToDos.ToList();
        }

        public void CreateToDo(ToDo toDo)
        {
            if (toDo == null)
            {
                throw new ArgumentNullException(nameof(toDo));
            }

            toDo.Id = NextId;
            NextId++;

            ToDos.Add(toDo);
        }

        public void DeleteToDo(ToDo toDo)
        {
            if (toDo == null)
            {
                throw new ArgumentNullException(nameof(toDo));
            }

            ToDos.Remove(toDo);
        }
    }