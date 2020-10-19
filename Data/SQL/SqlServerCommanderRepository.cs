using Commander.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Commander.Data.SQL
{
    public class SqlServerCommanderRepository : ICommanderRepository
    {
        private readonly CommanderContext _context;

        public SqlServerCommanderRepository(CommanderContext context)
        {
            _context = context;
        }

		public void CreateCommand(CommandModel command)
		{
         if( command == null )
			{
            throw new ArgumentNullException(nameof(command));
			}

         _context.Add(command);
		}

		public CommandModel LookupCommand(int id)
        {
            return _context.Commands.SingleOrDefault(_ => _.Id == id);
        }

        public IEnumerable<CommandModel> LookupCommands()
        {
            return _context.Commands.ToList();
        }

		public bool SaveChanges()
		{
         return _context.SaveChanges() >= 0;
		}
	}
}
