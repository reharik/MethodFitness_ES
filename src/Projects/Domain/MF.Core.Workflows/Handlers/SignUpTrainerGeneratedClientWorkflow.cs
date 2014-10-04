using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using MF.Core.Domain.AggregateRoots;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Command;
using Newtonsoft.Json;

namespace MF.Core.Workflows.Handlers
{
    public class SignUpHouseGeneratedClientWorkflow : HandlerBase, IHandler
    {
        public SignUpHouseGeneratedClientWorkflow(IMongoRepository mongoRepository, IGetEventStoreRepository getEventStoreRepository)
            : base(mongoRepository)
        {
            _repository = getEventStoreRepository;
            register(typeof(SignUpHouseGeneratedClient), archiveUser);
        }

        private Client archiveUser(IGESEvent x)
        {
            var vent = (SignUpHouseGeneratedClient)x;
            var item = new Client();
            item.Handle(vent);
            return item;
        }
    }
}