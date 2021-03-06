﻿using AutoMapper;
using Commander.DataAccessLayer;
using Output = Commander.Models.External.Output;
using Input = Commander.Models.External.Input;
using Database = Commander.Models.Database;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;
using Commander.Models.External.Input.Command;
using Commander.Models.External.Output.Command;

namespace Commander.Services.Command
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

		public IEnumerable<CommandReadModel> LookupCommands()
		{
			var commandItems = _dataAccessLayer.LookupCommands();

			return _mapper.Map<IEnumerable<CommandReadModel>>(commandItems);
		}

		public bool LookupCommand(int id, out CommandReadModel model)
		{
			var returnValue = _dataAccessLayer.LookupCommand(id, out var databaseModel);

			if (!returnValue)
			{
				model = null;
				return false;
			}

			model = _mapper.Map<CommandReadModel>(databaseModel);
			return true;
		}

		public bool Add(CommandCreateModel createModel, out CommandReadModel createdCommand)
		{
			var commandDatabaseModel = _mapper.Map<Database.CommandModel>(createModel);

			try
			{
				_dataAccessLayer.Create(commandDatabaseModel);
				_dataAccessLayer.Save();
			}
			catch (Exception exception)
			{
				//TODO: Beef this up for real production environment
				//Poor Man's Logging until full logging implemented
				_logger.LogError("Something went Boom", exception);
				createdCommand = null;
				return false;
			}

			createdCommand = _mapper.Map<CommandReadModel>(commandDatabaseModel);
			return true;
		}

		public bool Update(int id, CommandUpdateModel commandUpdateModel)
		{
			if (!_dataAccessLayer.LookupCommand(id, out var foundCommand))
			{
				return false;
			}

			_mapper.Map(commandUpdateModel, foundCommand);
			_dataAccessLayer.Update(foundCommand);
			_dataAccessLayer.Save();

			return true;
		}

		public bool Delete(int id, out CommandReadModel deletedCommand)
		{
			if (!_dataAccessLayer.LookupCommand(id, out var foundCommand))
			{
				deletedCommand = null;
				return false;
			}

			_dataAccessLayer.Delete(foundCommand);
			_dataAccessLayer.Save();

			deletedCommand = _mapper.Map<CommandReadModel>(foundCommand);
			return true;
		}
	}
}
