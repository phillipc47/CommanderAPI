using Commander.Models.Database;
using System.Collections.Generic;

namespace Commander.Data
{
    public class HardCodedCommanderRepository : ICommanderRepository
    {
		public void CreateCommand(CommandModel command)
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
                Platform = "Bash"
            };
        }

        public IEnumerable<CommandModel> LookupCommands()
        {
            return new List<CommandModel>
            {
                new CommandModel {Id = 0, HowTo = "Make a Directory", Line = "mkdir <directory name>", Platform = "Bash" },
                new CommandModel {Id = 1, HowTo = "Install Fortune", Line = "apt-get install fortune", Platform = "Bash" },
                new CommandModel {Id = 2, HowTo = "Install Cowsay", Line = "yum install cowsay", Platform = "Bash"}
            };
        }

		public bool SaveChanges()
		{
			throw new System.NotImplementedException();
		}
	}
}
