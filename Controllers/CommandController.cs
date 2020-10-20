using System.Collections.Generic;
using Output = Commander.Models.External.Output;
using Input = Commander.Models.External.Input;
using Commander.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Commander.Models.External.Input;
using Commander.DataAccessLayer;
using AutoMapper;

namespace Commander.Controllers
{
	//TODO: I know, right now the id is database specific and it needs to be entity specific (as in uri)

	[Route("api/commands")]
	[ApiController]
	public class CommandController : ControllerBase
	{
		private readonly ICommandService _service;
		private readonly ICommandDataAccessLayer _dataAccessLayer;
		private readonly IMapper _mapper;

		public CommandController(ICommandService service, ICommandDataAccessLayer dataAccessLayer, IMapper mapper)
		{
			_service = service;
			_dataAccessLayer = dataAccessLayer;
			_mapper = mapper;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Output.CommandReadModel>> LookupCommands()
		{
			var commandItems = _service.LookupCommands();

			return Ok(commandItems);
		}

		[HttpGet("{id}", Name="LookupCommand")]
		public ActionResult<Output.CommandReadModel> LookupCommand(int id)
		{
			if (_service.LookupCommand(id, out var command))
			{
				return Ok(command);
			}

			return NotFound();
		}

		[HttpPost]
		public ActionResult<Output.CommandReadModel> AddCommand(Input.CommandCreateModel command)
		{
			//Basic Validatation happens through Annotations on Create Command Model

			if( !_service.Add(command, out var createdCommand) )
			{
				//TODO: Wouldnt do this for real in production environment, but, until we get round to it
				return new NoContentResult();
			}

			//TODO: Don't leak the database Id, rather use the restful entity id
			return CreatedAtRoute(nameof(LookupCommand), new { Id = createdCommand.Id }, createdCommand);
		}

		// Yep, older, larger over the wire, can be error prone based upon size of object, but for this entity, it's a decent fit
		[HttpPut("{id}")]
		public ActionResult UpdateCommand(int id, Input.CommandUpdateModel commandUpdateModel)
		{
			if( !_service.Update(id, commandUpdateModel) )
			{
				return NotFound();
			}

			return NoContent();
		}

		//TODO: Do not like the direct data access layer usage from this level; future iteration 
		[HttpPatch("{id}")]
		public ActionResult PatchCommand(int id, JsonPatchDocument<CommandUpdateModel> patchDocument)
		{
			// Can we find the command?
			if( !_dataAccessLayer.LookupCommand(id, out var dataCommand))
			{
				return NotFound();
			}

			// Use the patchDocument and apply changes
			var commandToPatch = _mapper.Map<CommandUpdateModel>(dataCommand);
			patchDocument.ApplyTo(commandToPatch, ModelState);

			if( !TryValidateModel(commandToPatch) )
			{
				return ValidationProblem(ModelState);
			}

			// Now that the document is applied, store down to the data layer
			_mapper.Map(commandToPatch, dataCommand);

			_dataAccessLayer.Update(dataCommand);
			_dataAccessLayer.Save();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			if( !_service.Delete(id, out var deletedCommand) )
			{
				return NotFound();
			}

			return Ok(deletedCommand);
		}
	}
}
