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
		public DbSet<CommandCategoryModel> Categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CommandModel>()
				.HasOne(_ => _.Category);

			modelBuilder.Entity<CommandCategoryModel>()
				.HasMany(_ => _.Commands);

			DatabaseInitializer.Seed(modelBuilder);
		}
	}
}
