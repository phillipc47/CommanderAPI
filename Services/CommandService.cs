using AutoMapper;
using Commander.DataAccessLayer;
using Commander.Models.External.Output;
using System.Collections.Generic;

namespace Commander.Services
{
    public class CommandService : ICommandService
    {
        private readonly ICommandDataAccessLayer _dataAccessLayer;
        private readonly IMapper _mapper;

        public CommandService(ICommandDataAccessLayer dataAccessLayer, IMapper mapper)
        {
            _dataAccessLayer = dataAccessLayer;
            _mapper = mapper;
        }

        public IEnumerable<CommandModel> LookupCommands()
        {
            var commandItems = _dataAccessLayer.LookupCommands();

            return _mapper.Map<IEnumerable<CommandModel>>(commandItems);
        }

        public bool LookupCommand( int id, out CommandModel model )
        {
            var returnValue = _dataAccessLayer.LookupCommand(id, out var databaseModel);
            
            if( !returnValue )
            {
                model = null;
                return false;
            }

            model = _mapper.Map<CommandModel>(databaseModel);
            return true;
        }
    }
}
