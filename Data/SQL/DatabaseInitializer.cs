using Commander.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Commander.Data.SQL
{
	public static class DatabaseInitializer
	{
		// Wow, hate this hardcoded id stuff-o-la with seed data and code first in EF Core
		internal static class IdentityValues
		{
			public static int CategoryId = 1;
			public static int CommandId = 1;
		}

		public static void Seed(ModelBuilder modelBuilder)
		{
			var efCoreCategory = AddCategory(modelBuilder, "EF Core");
			AddCommand(modelBuilder, "Create Migrations", "dotnet ef migrations add <Name>", efCoreCategory);
			AddCommand(modelBuilder, "Run Migrations", "dotnet ef database update", efCoreCategory);

			var userSecretsCategory = AddCategory(modelBuilder, "User Secrets");
			AddCommand(modelBuilder, "Create User Secret", "dotnet user-secrets set <Key> <Value>", userSecretsCategory);
			AddCommand(modelBuilder, "List All User Secret", "dotnet user-secrets list", userSecretsCategory);
		}

		private static CommandModel AddCommand(ModelBuilder modelBuilder, string howTo, string line, CommandCategoryModel category)
		{
			var command = new CommandModel
			{
				// This feels so wrong--but according to the Microsoft docs this is how you do code first seeding
				Id = IdentityValues.CommandId++,
				HowTo = howTo,
				Line = line,
				CategoryId = category.Id
			};

			modelBuilder.Entity<CommandModel>().HasData(command);

			return command;
		}

		private static CommandCategoryModel AddCategory(ModelBuilder modelBuilder, string description)
		{
			var category = new CommandCategoryModel
			{
				Id = IdentityValues.CategoryId++,
				Description = $"{description}"
			};

			modelBuilder.Entity<CommandCategoryModel>().HasData(category);

			return category;
		}
	}
}
