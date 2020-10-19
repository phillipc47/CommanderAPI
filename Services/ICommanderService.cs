using Output = Commander.Models.External.Output;
using Input = Commander.Models.External.Input;
using System.Collections.Generic;
using Commander.Models.External.Input;

namespace Commander.Services
{
	public interface ICommandService
	{
		IEnumerable<Output.CommandReadModel> LookupCommands();

		bool LookupCommand(int id, out Output.CommandReadModel model);
		bool Add(Input.CommandCreateModel command, out Output.CommandReadModel createdCommand);
		bool Update(int id, CommandUpdateModel command);
		bool Delete(int id, out Output.CommandReadModel deletedCommand);
	}
}
