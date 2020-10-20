using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Output = Commander.Models.External.Output;
using Input = Commander.Models.External.Input;
using Commander.Services.Category;

namespace Commander.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryService _service;

		public CategoriesController(ICategoryService service)
		{
			_service = service;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Output.CategoryReadModel>> LookupCategories()
		{
			var commandItems = _service.LookupCategories();

			return Ok(commandItems);
		}

		[HttpGet("{id}", Name = "LookupCategory")]
		public ActionResult<Output.CommandReadModel> LookupCategory(int id)
		{
			if (_service.LookupCategory(id, out var command))
			{
				return Ok(command);
			}

			return NotFound();
		}

		[HttpPost]
		public ActionResult<Output.CategoryReadModel> AddCommand(Input.CategoryCreateModel category)
		{
			//Basic Validatation happens through Annotations on Create Command Model

			if (!_service.Add(category, out var createdCategory))
			{
				return new NoContentResult();
			}

			//TODO: Don't leak the database Id, rather use the restful entity id
			return CreatedAtRoute(nameof(LookupCategory), new { Id = createdCategory.Id }, createdCategory);
		}

		[HttpDelete("{id}")]
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
