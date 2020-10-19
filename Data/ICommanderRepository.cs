using Commander.Models;
using Commander.Models.Database;
using System.Collections.Generic;

namespace Commander.Data
{
    public interface ICommanderRepository
    {
        IEnumerable<CommandModel> LookupCommands();
        CommandModel LookupCommand(int id);
    }
}
