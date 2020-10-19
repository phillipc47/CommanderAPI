using Commander.Models.Database;
using System.Collections.Generic;

namespace Commander.DataAccessLayer
{
	public interface ICommandDataAccessLayer
	{
		IEnumerable<CommandModel> LookupCommands();
		bool LookupCommand(int id, out CommandModel commandItem);
		CommandModel Create(CommandModel commandItem);
		void Update(CommandModel command);
		void Delete(CommandModel command);
		bool Save();
	}
}
