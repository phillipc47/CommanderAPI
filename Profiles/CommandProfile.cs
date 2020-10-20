using AutoMapper;
using DatabaseModel = Commander.Models.Database;
using OutputModel = Commander.Models.External.Output;
using InputModel = Commander.Models.External.Input;
using Commander.Models.External.Input.Command;
using Commander.Models.External.Output.Command;

namespace Commander.Profiles
{
	public class CommandProfile : Profile
	{
		public CommandProfile()
		{
			CreateMap<DatabaseModel.CommandModel, CommandReadModel>()
				.ForMember( destination => destination.Category, 
								option => option.MapFrom(source => source.Category.Description));


			CreateMap<CommandCreateModel, DatabaseModel.CommandModel>();
			CreateMap<CommandUpdateModel, DatabaseModel.CommandModel>();
			CreateMap<DatabaseModel.CommandModel, CommandUpdateModel>();
		}
	}
}
