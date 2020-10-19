using AutoMapper;
using Commander.DataAccessLayer;
using Output = Commander.Models.External.Output;
using Input = Commander.Models.External.Input;
using Database = Commander.Models.Database;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;

namespace Commander.Services
{
	public class CommandService : ICommandService
	{
		private readonly ICommandDataAccessLayer _dataAccessLayer;
		private readonly IMapper _mapper;
		private readonly ILogger<CommandService> _logger;

		public CommandService(ICommandDataAccessLayer dataAccessLayer, IMapper mapper, ILogger<CommandService> logger)
		{
			_dataAccessLayer = dataAccessLayer;
			_mapper = mapper;
			_logger = logger;
		}

		public IEnumerable<Output.CommandReadModel> LookupCommands()
		{
			var commandItems = _dataAccessLayer.LookupCommands();

			return _mapper.Map<IEnumerable<Output.CommandReadModel>>(commandItems);
		}

		public bool LookupCommand(int id, out Output.CommandReadModel model)
		{
			var returnValue = _dataAccessLayer.LookupCommand(id, out var databaseModel);

			if (!returnValue)
			{
				model = null;
				return false;
			}

			model = _mapper.Map<Output.CommandReadModel>(databaseModel);
			return true;
		}

		public bool Add(Input.CommandCreateModel createModel, out Output.CommandReadModel createdCommand)
		{
			var commandDatabaseModel = _mapper.Map<Database.CommandModel>(createModel);

			try
			{
				_dataAccessLayer.Create(commandDatabaseModel);
				_dataAccessLayer.Save();
			}
			catch(Exception exception)
			{
				//TODO: Beef this up for real production environment
				//Poor Man's Logging until full logging implemented
				_logger.LogError("Something went Boom", exception);
				createdCommand = null;
				return false;
			}

			createdCommand = _mapper.Map<Output.CommandReadModel>(commandDatabaseModel);
			return true;
		}
	}
}
