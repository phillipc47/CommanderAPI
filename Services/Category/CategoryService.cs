using System;
using AutoMapper;
using Commander.Data;
using System.Collections.Generic;
using Output = Commander.Models.External.Output;
using Input = Commander.Models.External.Input;
using Database = Commander.Models.Database;
using Microsoft.Extensions.Logging;

namespace Commander.Services.Category
{
	public class CategoryService : ICategoryService
	{
		private readonly ICommanderRepository _repository;
		private readonly IMapper _mapper;
		private readonly ILogger<CategoryService> _logger;

		public CategoryService(ICommanderRepository repository, IMapper mapper, ILogger<CategoryService> logger)
		{
			// Iterations -- doing this without the DAL to demonstrate one less layer of isolation
			_repository = repository;
			_mapper = mapper;
			_logger = logger;
		}

		public bool Add(Input.CategoryCreateModel category, out Output.CategoryReadModel createdCategory)
		{
			var commandDatabaseModel = _mapper.Map<Database.CategoryModel>(category);

			try
			{
				_repository.Create(commandDatabaseModel);
				_repository.SaveChanges();
			}
			catch (Exception exception)
			{
				//TODO: Beef this up for real production environment
				_logger.LogError("Something went Boom", exception);
				createdCategory = null;
				return false;
			}

			createdCategory = _mapper.Map<Output.CategoryReadModel>(commandDatabaseModel);
			return true;
		}

		public IEnumerable<Output.CategoryReadModel> LookupCategories()
		{
			var categories = _repository.LookupCategories();
			return _mapper.Map<IEnumerable<Output.CategoryReadModel>>(categories);
		}

		public bool LookupCategory( int id, out Output.CategoryReadModel category )
		{
			var foundCategory = _repository.LookupCategory(id);

			if(foundCategory == null )
			{
				category = null;
				return false;
			}

			category = _mapper.Map<Output.CategoryReadModel>(foundCategory);
			return true;
		}

		public bool Delete(int id, out Output.CategoryReadModel deletedCategory)
		{
			var foundCategory = _repository.LookupCategory(id);
			if (foundCategory == null )
			{
				deletedCategory = null;
				return false;
			}

			_repository.Delete(foundCategory);
			_repository.SaveChanges();

			deletedCategory = _mapper.Map<Output.CategoryReadModel>(foundCategory);
			return true;
		}
	}
}
