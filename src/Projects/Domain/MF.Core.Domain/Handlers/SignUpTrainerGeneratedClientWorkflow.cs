using System;
using MF.Core.Domain.AggregateRoots;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.BaseClasses;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Command;

namespace MF.Core.Domain.Handlers
{
    public class SignUpTrainerGeneratedClientWorkflow : HandlerBase, IHandler
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;
        private readonly ILogger _logger;

        public SignUpTrainerGeneratedClientWorkflow(IMongoRepository mongoRepository, IGetEventStoreRepository getEventStoreRepository, IUIResponsePoster uiResponsePoster, ILogger logger)
            : base(mongoRepository, uiResponsePoster)
        {
            _getEventStoreRepository = getEventStoreRepository;
            _logger = logger;
            register(typeof(SignUpTrainerGeneratedClient), signUpTrainerGeneratedClient);
        }

        private void signUpTrainerGeneratedClient(IGESEvent x)
        {
            _logger.LogInfo("Processing Event: {0}", x.EventType);
            var signUpNewClient = (SignUpTrainerGeneratedClient)x;
            var client = new Client();
            client.Handle(signUpNewClient);
            _getEventStoreRepository.Save(client, Guid.NewGuid());
            _logger.LogInfo("Event {0} saved to GES", x.EventType);
        }
    }
}