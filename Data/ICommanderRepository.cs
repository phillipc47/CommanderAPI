using Commander.Models.Database;
using System.Collections.Generic;

namespace Commander.Data
{
   public interface ICommanderRepository
   {
      bool SaveChanges();

      CommandModel LookupCommand(int id);
      IEnumerable<CommandModel> LookupCommands();
      void CreateCommand(CommandModel command);
   }
}
