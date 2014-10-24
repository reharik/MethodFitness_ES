﻿using System;
using MF.Core.Domain.AggregateRoots;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Command;

namespace MF.Core.Domain.Handlers
{
    public class SignUpHouseGeneratedClientWorkflow : HandlerBase, IHandler
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;

        public SignUpHouseGeneratedClientWorkflow(IMongoRepository mongoRepository, IGetEventStoreRepository getEventStoreRepository)
            : base(mongoRepository)
        {
            _getEventStoreRepository = getEventStoreRepository;
            register(typeof(SignUpHouseGeneratedClient), archiveUser);
        }

        private void archiveUser(IGESEvent x)
        {
            var vent = (SignUpHouseGeneratedClient)x;
            var item = new Client();
            item.Handle(vent);
            _getEventStoreRepository.Save(item, Guid.NewGuid());
        }
    }
}