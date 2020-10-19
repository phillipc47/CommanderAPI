using Commander.Models.External.Output;
using System.Collections.Generic;

namespace Commander.Services
{
    public interface ICommandService
    {
        IEnumerable<CommandModel> LookupCommands();

        bool LookupCommand(int id, out CommandModel model);
    }
}
