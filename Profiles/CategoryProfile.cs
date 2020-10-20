using AutoMapper;
using DatabaseModel = Commander.Models.Database;
using OutputModel = Commander.Models.External.Output;
using InputModel = Commander.Models.External.Input;

namespace Commander.Profiles
{
	public class CategoryProfile : Profile
	{
		public CategoryProfile()
		{
			CreateMap<DatabaseModel.CategoryModel, OutputModel.CategoryReadModel>();
			CreateMap<InputModel.CategoryCreateModel, DatabaseModel.CategoryModel>();
		}
	}
}
