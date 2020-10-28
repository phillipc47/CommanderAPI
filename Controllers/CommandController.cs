using System.Net;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Commander.DataAccessLayer;
using AutoMapper;
using Commander.Services.Command;
using Swashbuckle.AspNetCore.Annotations;
using Input = Commander.Models.External.Input.Command;
using Output = Commander.Models.External.Output.Command;

namespace Commander.Controllers
{
	//TODO: I know, right now the id is database specific and it needs to be entity specific (as in uri)

	[Produces("application/json")]
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
		[SwaggerOperation("Retrieve all Stored Commands")]
		[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Output.CommandReadModel>))]
		public ActionResult<IEnumerable<Output.CommandReadModel>> LookupCommands()
		{
			var commandItems = _service.LookupCommands();

			return Ok(commandItems);
		}

		[HttpGet("{id}", Name = "LookupCommand")]
		[SwaggerOperation("Retrieve a single command")]
		[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Output.CommandReadModel))]
		[SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Returned when the specified command id is not located")]
		public ActionResult<Output.CommandReadModel> LookupCommand(int id)
		{
			if (_service.LookupCommand(id, out var command))
			{
				return Ok(command);
			}

			return NotFound();
		}

		[HttpPost]
		[SwaggerOperation("Creates a new command")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Returned when the create model is not valid")]
		//TODO: Return Ok with Location
		[SwaggerResponse((int)HttpStatusCode.NoContent, Description = "Model is created -- location in header")]
		public ActionResult<Output.CommandReadModel> AddCommand(Input.CommandCreateModel command)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (!_service.Add(command, out var createdCommand))
			{
				//TODO: Wouldnt do this for real in production environment, but, until we get round to it
				return new NoContentResult();
			}

			//TODO: Don't leak the database Id, rather use the restful entity id
			return CreatedAtRoute(nameof(LookupCommand), new { Id = createdCommand.Id }, createdCommand);
		}

		// Yep, older, larger over the wire, can be error prone based upon size of object, but for this entity, it's a decent fit
		[HttpPut("{id}")]
		[SwaggerOperation("Update an existing command")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Returned when the update model is not valid")]
		[SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Returned when the command to update could not be located by the specified identifier")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, Description = "Model is updated")]
		public ActionResult UpdateCommand(int id, Input.CommandUpdateModel commandUpdateModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (!_service.Update(id, commandUpdateModel))
			{
				return NotFound();
			}

			return NoContent();
		}

		//TODO: Do not like the direct data access layer usage from this level; future iteration 
		[HttpPatch("{id}")]
		[SwaggerOperation("Patch an existing command")]
		[SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Returned when the command to be patched could not be located by the specified identifier")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, Description = "Model is properly patched")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Returned when the patch document is not valid")]
		public ActionResult PatchCommand(int id, JsonPatchDocument<Input.CommandUpdateModel> patchDocument)
		{
			// Can we find the command?
			if (!_dataAccessLayer.LookupCommand(id, out var dataCommand))
			{
				return NotFound();
			}

			// Use the patchDocument and apply changes
			var commandToPatch = _mapper.Map<Input.CommandUpdateModel>(dataCommand);
			patchDocument.ApplyTo(commandToPatch, ModelState);

			if (!TryValidateModel(commandToPatch))
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
		[SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Returned when the command could not be located by the specified identifier")]
		[SwaggerResponse((int)HttpStatusCode.OK, Description = "Command is deleted")]
		public ActionResult Delete(int id)
		{
			if (!_service.Delete(id, out var deletedCommand))
			{
				return NotFound();
			}

			return Ok(deletedCommand);
		}
	}
}
