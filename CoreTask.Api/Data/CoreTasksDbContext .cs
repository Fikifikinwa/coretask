using CoreTask.Api  .Model;
using Microsoft.EntityFrameworkCore;

namespace CoreTask.Api.Data
{
    public class CoreTasksDbContext(DbContextOptions<CoreTasksDbContext> options) : DbContext(options)
    {
        public DbSet<TodoTask> TodoTasks { get; set; }
    }
}
