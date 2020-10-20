using System.Collections.Generic;
using Output = Commander.Models.External.Output;
using Input = Commander.Models.External.Input.Category;

namespace Commander.Services.Category
{
	public interface ICategoryService
	{
		IEnumerable<Output.CategoryReadModel> LookupCategories();
		bool LookupCategory(int id, out Output.CategoryReadModel category);
		bool Add(Input.CategoryCreateModel category, out Output.CategoryReadModel createdCategory);
		bool Delete(int id, out Output.CategoryReadModel deletedCategory);
		bool Update(int id, Input.CategoryUpdateModel category);
	}
}
