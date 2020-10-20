using Output = Commander.Models.External.Output;
using Input = Commander.Models.External.Input;
using System.Collections.Generic;
using Commander.Models.External.Input.Command;
using Commander.Models.External.Output.Command;

namespace Commander.Services.Command
{
	public interface ICommandService
	{
		IEnumerable<CommandReadModel> LookupCommands();

		bool LookupCommand(int id, out CommandReadModel model);
		bool Add(CommandCreateModel command, out CommandReadModel createdCommand);
		bool Update(int id, CommandUpdateModel command);
		bool Delete(int id, out CommandReadModel deletedCommand);
	}
}
