using MF.Core.Domain.AggregateRoots;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Command;

namespace MF.Core.Domain.Handlers
{
    public class SignUpTrainerGeneratedClientWorkflow : WorkflowBase, IHandler
    {
        public SignUpTrainerGeneratedClientWorkflow(IMongoRepository mongoRepository, IGetEventStoreRepository getEventStoreRepository)
            : base(getEventStoreRepository, mongoRepository)
        {
            register(typeof(SignUpTrainerGeneratedClient), signUpTrainerGeneratedClient);
        }

        private Client signUpTrainerGeneratedClient(IGESEvent x)
        {
            var signUpNewClient = (SignUpTrainerGeneratedClient)x;
            var client = new Client();
            client.Handle(signUpNewClient);
            return client;
        }
    }
}