using System.Collections.Generic;
using Output = Commander.Models.External.Output;
using Input = Commander.Models.External.Input;
using Commander.Services;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommandsController : ControllerBase
	{
		private readonly ICommandService _service;

		public CommandsController(ICommandService service)
		{
			_service = service;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Output.CommandReadModel>> LookupCommands()
		{
			var commandItems = _service.LookupCommands();

			return Ok(commandItems);
		}

		//TODO: I know, right now the id is database specifc
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
	}
}
