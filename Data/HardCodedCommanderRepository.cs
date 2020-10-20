using Commander.Models.Database;
using System.Collections.Generic;

namespace Commander.Data
{
	public class HardCodedCommanderRepository : ICommanderRepository
	{
		private const int BashCategoryId = 12;

		public void Create(CommandModel command)
		{
			throw new System.NotImplementedException();
		}

		public void Create(CategoryModel category)
		{
			throw new System.NotImplementedException();
		}

		public void Delete(CommandModel command)
		{
			throw new System.NotImplementedException();
		}

		public void Delete(CategoryModel category)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<CategoryModel> LookupCategories()
		{
			throw new System.NotImplementedException();
		}

		public CategoryModel LookupCategory(int id)
		{
			throw new System.NotImplementedException();
		}

		public CommandModel LookupCommand(int id)
		{
			return new CommandModel()
			{
				Id = 0,
				HowTo = "Make a Directory",
				Line = "mkdir <directory name>",
				CategoryId = BashCategoryId
			};
		}
		public IEnumerable<CommandModel> LookupCommands()

		{
			return new List<CommandModel>
				{
					 new CommandModel {Id = 0, HowTo = "Make a Directory", Line = "mkdir <directory name>", CategoryId = BashCategoryId },
					 new CommandModel {Id = 1, HowTo = "Install Fortune", Line = "apt-get install fortune", CategoryId = BashCategoryId },
					 new CommandModel {Id = 2, HowTo = "Install Cowsay", Line = "yum install cowsay", CategoryId = BashCategoryId }
				};
		}

		public bool SaveChanges()
		{
			throw new System.NotImplementedException();
		}

		public void Update(CommandModel command)
		{
			throw new System.NotImplementedException();
		}
	}
}
