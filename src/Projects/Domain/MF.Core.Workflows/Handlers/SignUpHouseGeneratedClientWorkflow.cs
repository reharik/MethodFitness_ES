using System;
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
        private readonly IGetEventStoreRepository _getEventStoreRepository;

        public SignUpTrainerGeneratedClientWorkflow(IGetEventStoreRepository getEventStoreRepository, IMongoRepository mongoRepository)
            : base(mongoRepository)
        {
            _getEventStoreRepository = getEventStoreRepository;
        }

        public bool HandlesEvent(IGESEvent @event)
        {
            return @event.EventType == typeof(SignUpTrainerGeneratedClient).Name;
        }

        public ActionBlock<IGESEvent> ReturnActionBlock()
        {
            return new ActionBlock<IGESEvent>(x =>
            {
                if (ExpectEventPositionIsGreaterThanLastRecorded(x)) { return; };

                var signUpNewClient = (SignUpTrainerGeneratedClient)x;
                var client = new Client(true);
                client.Handle(signUpNewClient);
                _getEventStoreRepository.Save(client, Guid.NewGuid());
                SetEventAsRecorded(x);
            });
        }
    }
}