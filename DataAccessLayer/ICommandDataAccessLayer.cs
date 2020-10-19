using Commander.Models.Database;
using System.Collections.Generic;

namespace Commander.DataAccessLayer
{
    public interface ICommandDataAccessLayer
    {
        IEnumerable<CommandModel> LookupCommands();
        bool LookupCommand(int id, out CommandModel commandItem);
    }
}
