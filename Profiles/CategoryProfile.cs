using AutoMapper;
using DatabaseModel = Commander.Models.Database;
using OutputModel = Commander.Models.External.Output;
using InputModel = Commander.Models.External.Input;
using Commander.Models.External.Input.Category;
using Commander.Models.External.Output.Category;

namespace Commander.Profiles
{
	public class CategoryProfile : Profile
	{
		public CategoryProfile()
		{
			CreateMap<DatabaseModel.CategoryModel, CategoryReadModel>();
			CreateMap<CategoryCreateModel, DatabaseModel.CategoryModel>();
		}
	}
}
