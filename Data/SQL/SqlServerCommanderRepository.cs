using Commander.Models.Database;
using System.Collections.Generic;
using System.Linq;

namespace Commander.Data.SQL
{
    public class SqlServerCommanderRepository : ICommanderRepository
    {
        private readonly CommanderContext _context;

        public SqlServerCommanderRepository(CommanderContext context)
        {
            _context = context;
        }

        public CommandModel LookupCommand(int id)
        {
            return _context.Commands.SingleOrDefault(_ => _.Id == id);
        }

        public IEnumerable<CommandModel> LookupCommands()
        {
            return _context.Commands.ToList();
        }
    }
}
