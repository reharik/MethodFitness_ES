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
    public class SignUpTrainerGeneratedClientWorkflow : HandlerBase, IHandler
    {
        public SignUpTrainerGeneratedClientWorkflow(IMongoRepository mongoRepository, IGetEventStoreRepository getEventStoreRepository)
            : base(mongoRepository)
        {
            _repository = getEventStoreRepository;
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