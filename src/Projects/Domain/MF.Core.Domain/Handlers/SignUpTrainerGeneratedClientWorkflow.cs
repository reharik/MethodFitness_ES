using System;
using MF.Core.Domain.AggregateRoots;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Command;

namespace MF.Core.Domain.Handlers
{
    public class SignUpTrainerGeneratedClientWorkflow : HandlerBase, IHandler
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;

        public SignUpTrainerGeneratedClientWorkflow(IMongoRepository mongoRepository, IGetEventStoreRepository getEventStoreRepository, IUIResponsePoster uiResponsePoster)
            : base(mongoRepository, uiResponsePoster)
        {
            _getEventStoreRepository = getEventStoreRepository;
            register(typeof(SignUpTrainerGeneratedClient), signUpTrainerGeneratedClient);
        }

        private void signUpTrainerGeneratedClient(IGESEvent x)
        {
            var signUpNewClient = (SignUpTrainerGeneratedClient)x;
            var client = new Client();
            client.Handle(signUpNewClient);
            _getEventStoreRepository.Save(client, Guid.NewGuid());
        }
    }
}