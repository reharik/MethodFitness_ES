using System;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Events;
using MF.Core.ReadModel.Model;
using Newtonsoft.Json;

namespace MF.Core.ReadModelEventHandler.Handlers
{
    public class ClientSummaryHandler : HandlerBase, IHandler
    {
        public ClientSummaryHandler(IMongoRepository mongoRepository)
            : base(mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public bool HandlesEvent(IGESEvent @event)
        {
            if (@event.EventType ==typeof(HouseGeneratedClientSignedUp).Name) { return true; }
            if (@event.EventType == typeof(TrainerGeneratedClientSignedUp).Name) { return true; }
            return false;
        }

        public ActionBlock<IGESEvent> ReturnActionBlock()
        {
            return new ActionBlock<IGESEvent>(x =>
            {
                switch (x.EventType)
                {
                    case "HouseGeneratedClientSignedUp":
                        HandleEvent(x, HandleHouseGenerated);
                        break;
                    case "TrainerGeneratedClientSignedUp":
                        HandleEvent(x, HandleTrainerGenerated);
                        break;
                }
            }, new ExecutionDataflowBlockOptions()
            {
                MaxDegreeOfParallelism = 4
            });
        }

        private IReadModel HandleHouseGenerated(IGESEvent x)
        {
            var clientSignedUp = (HouseGeneratedClientSignedUp)x;
            var client = new ClientSummary();
            client.FirstName = clientSignedUp.FirstName;
            client.LastName = clientSignedUp.LastName;
            client.EmailAddress = clientSignedUp.EmailAddress;
            client.Phone = clientSignedUp.Phone;
            return client;
        }

        private IReadModel HandleTrainerGenerated(IGESEvent x)
        {
            var clientSignedUp = (TrainerGeneratedClientSignedUp)x;
            var client = new ClientSummary();
            client.FirstName = clientSignedUp.FirstName;
            client.LastName = clientSignedUp.LastName;
            client.EmailAddress = clientSignedUp.EmailAddress;
            client.Phone = clientSignedUp.Phone;
            return client;
        }
    }
}