using System.Collections.Generic;
using Output = Commander.Models.External.Output;
using Input = Commander.Models.External.Input.Category;
using Commander.Models.External.Output.Category;

namespace Commander.Services.Category
{
	public interface ICategoryService
	{
		IEnumerable<CategoryReadModel> LookupCategories();
		bool LookupCategory(int id, out CategoryReadModel category);
		bool Add(Input.CategoryCreateModel category, out CategoryReadModel createdCategory);
		bool Delete(int id, out CategoryReadModel deletedCategory);
		bool Update(int id, Input.CategoryUpdateModel category);
	}
}
