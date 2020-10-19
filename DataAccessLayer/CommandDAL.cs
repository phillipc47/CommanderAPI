using Commander.Data;
using Commander.Models.Database;
using System.Collections.Generic;

namespace Commander.DataAccessLayer
{
    public class CommandDAL : ICommandDataAccessLayer
    {
        private readonly ICommanderRepository _repository;

        public CommandDAL(ICommanderRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CommandModel> LookupCommands()
        {
            var commandItems = _repository.LookupCommands();
            return commandItems;
        }

        public bool LookupCommand(int id, out CommandModel commandItem)
        {
            commandItem = _repository.LookupCommand(id);

            if (commandItem == null)
            {
                return false;
            }
            return true;
        }
    }
}
}
