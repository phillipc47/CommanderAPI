using Commander.Models.Database;
using System.Collections.Generic;

namespace Commander.Data
{
   public interface ICommanderRepository
   {
      bool SaveChanges();

      CommandModel Lookup(int id);
      IEnumerable<CommandModel> Lookup();
      void Create(CommandModel command);
      void Update(CommandModel command);
      void Delete(CommandModel command);
   }
}
