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

		public void Create(CategoryModel category)
		{
			if( category == null )
			{
				throw new ArgumentNullException(nameof(category));
			}

			_context.Add(category);
		}

		public void Delete(CommandModel command)
		{
         if( command == null )
			{
            throw new ArgumentNullException(nameof(command));
			}

         _context.Remove(command);
		}

		public void Delete(CategoryModel category)
		{
			if( category == null )
			{
				throw new ArgumentNullException(nameof(category));
			}

			_context.Remove(category);
		}


		public IEnumerable<CategoryModel> LookupCategories()
		{
			return _context.Categories.ToList();
		}

		public CategoryModel LookupCategory(int id)
		{
			return _context.Categories.SingleOrDefault(_ => _.Id == id);
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

		public void Update(CommandModel command)
		{
         // Do Nothing -- EF approach, grab the model, change it, save it
		}

		public void Update(CategoryModel category)
		{
			if( category == null )
			{
				throw new ArgumentNullException();
			}

			_context.Update(category);
		}
	}
}
