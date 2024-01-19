using Microsoft.EntityFrameworkCore;

namespace TodoMinimal.Model
{
    public class TodoDb: DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> option)
            : base(option) { }

        public DbSet<Todo> Todos => Set<Todo>();
    }
}
