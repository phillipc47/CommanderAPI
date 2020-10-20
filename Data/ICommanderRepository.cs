using Commander.Models.Database;
using System.Collections.Generic;

namespace Commander.Data
{
   public interface ICommanderRepository
   {
      bool SaveChanges();

      CommandModel LookupCommand(int id);
      IEnumerable<CommandModel> LookupCommands();

      CategoryModel LookupCategory(int id);
      IEnumerable<CategoryModel> LookupCategories();

      void Create(CategoryModel category);

      void Create(CommandModel command);
      void Update(CommandModel command);
      void Delete(CommandModel command);
      void Delete(CategoryModel category);
   }
}
