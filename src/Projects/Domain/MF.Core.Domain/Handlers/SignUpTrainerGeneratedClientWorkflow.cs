using MF.Core.Domain.AggregateRoots;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Command;

namespace MF.Core.Domain.Handlers
{
    public class SignUpHouseGeneratedClientWorkflow : WorkflowBase, IHandler
    {
        public SignUpHouseGeneratedClientWorkflow(IMongoRepository mongoRepository, IGetEventStoreRepository getEventStoreRepository)
            : base(getEventStoreRepository, mongoRepository)
        {
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