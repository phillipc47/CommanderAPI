using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Commander.Services.Category;
using Swashbuckle.AspNetCore.Annotations;
using Input = Commander.Models.External.Input.Category;
using Output = Commander.Models.External.Output.Category;

namespace Commander.Controllers
{
	[Route("api/categories")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _service;

		public CategoryController(ICategoryService service)
		{
			_service = service;
		}

		[HttpGet]
		[SwaggerOperation("Retrieve all categories")]
		[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Output.CategoryReadModel>))]
		public ActionResult<IEnumerable<Output.CategoryReadModel>> LookupCategories()
		{
			var commandItems = _service.LookupCategories();

			return Ok(commandItems);
		}

		[HttpGet("{id}", Name = "LookupCategory")]
		[SwaggerOperation("Retrieve a category with the specified id")]
		[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Output.CategoryReadModel))]
		public ActionResult<Output.CategoryReadModel> LookupCategory(int id)
		{
			if (_service.LookupCategory(id, out var command))
			{
				return Ok(command);
			}

			return NotFound();
		}

		[HttpPost]
		[SwaggerOperation("Creates a new category")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Returned when the create model is not valid")]
		//TODO: Return Ok with Location
		[SwaggerResponse((int)HttpStatusCode.NoContent, Description = "Returned when the model is created -- location in header")]
		public ActionResult<Output.CategoryReadModel> Add(Input.CategoryCreateModel category)
		{
			if( !ModelState.IsValid )
			{
				return BadRequest(ModelState);
			}

			if (!_service.Add(category, out var createdCategory))
			{
				return new NoContentResult();
			}

			//TODO: Don't leak the database Id, rather use the restful entity id
			return CreatedAtRoute(nameof(LookupCategory), new { Id = createdCategory.Id }, createdCategory);
		}

		[HttpDelete("{id}")]
		[SwaggerOperation("Delete an existing category")]
		[SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Returned when the category could not be located by the specified identifier")]
		[SwaggerResponse((int)HttpStatusCode.OK, Description = "Category is deleted")]
		public ActionResult Delete(int id)
		{
			if (!_service.Delete(id, out var deletedCommand))
			{
				return NotFound();
			}

			return Ok(deletedCommand);
		}

		[HttpPut("{id}")]
		[SwaggerOperation("Update an existing category")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Returned when the update model is not valid")]
		[SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Returned when the category to update could not be located by the specified identifier")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, Description = "Model is updated")]
		public ActionResult Update(int id, Input.CategoryUpdateModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (!_service.Update(id, model))
			{
				return NotFound();
			}

			return NoContent();
		}
	}
}
