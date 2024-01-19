namespace TodoMinimal.Model
{
    public class Todo
    {
        public int Id { get; set; }
        public string? TodoTask { get; set; }
        public bool IsCompleted { get; set; }  

    }

    public class TodoBody
    {
        public string? TodoTask { get; set; }
    }
}
