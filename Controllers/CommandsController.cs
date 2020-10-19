using System.Collections.Generic;
using Commander.Data;
using Commander.Models.External.Output;
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
        public ActionResult<IEnumerable<CommandModel>> LookupCommands()
        {
            var commandItems = _service.LookupCommands();

            return Ok(commandItems);
        }

        [HttpGet("{id}")]
        public ActionResult<CommandModel> LookupCommand(int id)
        {
            if( _service.LookupCommand(id, out var command) )
            {
                return Ok(command);
            }

            return NotFound();
        }
    }
}
