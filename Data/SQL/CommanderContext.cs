using Microsoft.EntityFrameworkCore;
using Commander.Models.Database;

namespace Commander.Data.SQL
{
    public class CommanderContext : DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> options) : base(options)
        {

        }

        public DbSet<CommandModel> Commands { get; set; }
    }
}
