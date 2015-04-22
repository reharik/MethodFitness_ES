﻿using System;
using System.Threading.Tasks;
using MF.Core.Domain.AggregateRoots;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.BaseClasses;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Command;

namespace MF.Core.Domain.Handlers
{
    public class ChangeClientEmailWorkflow : HandlerBase, IHandler
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;
        public ChangeClientEmailWorkflow(IMongoRepository mongoRepository,
            IGetEventStoreRepository getEventStoreRepository,
            IUIResponsePoster uiResponsePoster,
            ILogger logger)
            : base(mongoRepository, uiResponsePoster, logger)
        {
            _getEventStoreRepository = getEventStoreRepository;
            register(typeof(ChangeClientEmail), correctClientName);
        }

        private void correctClientName(IGESEvent x)
        {
            var _vent = (ChangeClientEmail)x;
            var client = _getEventStoreRepository.GetById<Client>(_vent.ClientId).Result;
            client.Handle(_vent);
            _getEventStoreRepository.Save(client, Guid.NewGuid(), _continuationId);
        }
    }
}