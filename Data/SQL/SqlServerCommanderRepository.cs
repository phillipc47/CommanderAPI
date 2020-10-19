using Commander.Models.Database;
using System;
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

		public void Create(CommandModel command)
		{
         if( command == null )
			{
            throw new ArgumentNullException(nameof(command));
			}

         _context.Add(command);
		}

		public void Delete(CommandModel command)
		{
         if( command == null )
			{
            throw new ArgumentNullException(nameof(command));
			}

         _context.Remove(command);
		}

		public CommandModel Lookup(int id)
        {
            return _context.Commands.SingleOrDefault(_ => _.Id == id);
        }

        public IEnumerable<CommandModel> Lookup()
        {
            return _context.Commands.ToList();
        }

		public bool SaveChanges()
		{
         return _context.SaveChanges() >= 0;
		}

		public void Update(CommandModel command)
		{
         // Do Nothing -- EF approach, grab the model, change it, save it
		}
	}
}
