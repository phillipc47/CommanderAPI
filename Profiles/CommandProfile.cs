using AutoMapper;
using System.Collections.Generic;
using DatabaseModel = Commander.Models.Database;
using OutputModel = Commander.Models.External.Output;

namespace Commander.Profiles
{
    public class CommandProfile : Profile
    {
        public CommandProfile()
        {
            CreateMap<DatabaseModel.CommandModel, OutputModel.CommandModel>();
        }
    }
}
