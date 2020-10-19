using Commander.Data;
using Commander.Models.Database;
using System.Collections.Generic;

namespace Commander.DataAccessLayer
{
	//TODO: Revisit from an architecture perspective; think the DAL + Repository is too much unnecessary abstraction for now
	public class CommandDAL : ICommandDataAccessLayer
	{
		private readonly ICommanderRepository _repository;

		public CommandDAL(ICommanderRepository repository)
		{
			_repository = repository;
		}

		public IEnumerable<CommandModel> LookupCommands()
		{
			var commandItems = _repository.Lookup();
			return commandItems;
		}

		public bool LookupCommand(int id, out CommandModel commandItem)
		{
			commandItem = _repository.Lookup(id);

			if (commandItem == null)
			{
				return false;
			}
			return true;
		}

		public CommandModel Create(CommandModel command)
		{
			_repository.Create(command);

			return command;
		}

		public void Update(CommandModel command)
		{
			_repository.Update(command);
		}

		public void Delete(CommandModel command)
		{
			_repository.Delete(command);
		}

		public bool Save()
		{
			return _repository.SaveChanges();
		}
	}
}
