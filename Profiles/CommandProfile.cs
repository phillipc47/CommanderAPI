using AutoMapper;
using DatabaseModel = Commander.Models.Database;
using OutputModel = Commander.Models.External.Output;
using InputModel = Commander.Models.External.Input;

namespace Commander.Profiles
{
	public class CommandProfile : Profile
	{
		public CommandProfile()
		{
			CreateMap<DatabaseModel.CommandModel, OutputModel.CommandReadModel>()
				.ForMember( destination => destination.Category, 
								option => option.MapFrom(source => source.Category.Description));


			CreateMap<InputModel.CommandCreateModel, DatabaseModel.CommandModel>();
			CreateMap<InputModel.CommandUpdateModel, DatabaseModel.CommandModel>();
			CreateMap<DatabaseModel.CommandModel, InputModel.CommandUpdateModel>();
		}
	}
}
