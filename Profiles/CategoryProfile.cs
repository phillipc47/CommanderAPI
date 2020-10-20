using AutoMapper;
using DatabaseModel = Commander.Models.Database;
using OutputModel = Commander.Models.External.Output;
using InputModel = Commander.Models.External.Input;
using Commander.Models.External.Input.Category;

namespace Commander.Profiles
{
	public class CategoryProfile : Profile
	{
		public CategoryProfile()
		{
			CreateMap<DatabaseModel.CategoryModel, OutputModel.CategoryReadModel>();
			CreateMap<CategoryCreateModel, DatabaseModel.CategoryModel>();
		}
	}
}
