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
    public class SignUpNewClientWorkflow : HandlerBase, IHandler
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;

        public SignUpNewClientWorkflow(IGetEventStoreRepository getEventStoreRepository, IMongoRepository mongoRepository)
            : base(mongoRepository)
        {
            _getEventStoreRepository = getEventStoreRepository;
            _handlerType = "SignUpNewClientWorkflow";
        }

        public bool HandlesEvent(IGESEvent @event)
        {
            return @event.EventType == "SignUpNewClient";
        }

        public ActionBlock<IGESEvent> ReturnActionBlock()
        {
            return new ActionBlock<IGESEvent>(x =>
            {
                if (ExpectEventPositionIsGreaterThanLastRecorded(x)) { return; };

                var signUpNewClient = (SignUpNewClient)x;
                var client = new Client(true);
                client.Handle(signUpNewClient);
                _getEventStoreRepository.Save(client, Guid.NewGuid());
                SetEventAsRecorded(x);
            });
        }
    }
}