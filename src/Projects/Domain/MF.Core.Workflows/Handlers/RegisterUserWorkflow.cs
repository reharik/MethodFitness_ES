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
    public class RegisterUserWorkflow : HandlerBase, IHandler
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;

        public RegisterUserWorkflow(IGetEventStoreRepository getEventStoreRepository, IMongoRepository mongoRepository)
            : base(mongoRepository)
        {
            _getEventStoreRepository = getEventStoreRepository;
            _handlerType = "RegisterUserWorkflow";
        }

        public bool HandlesEvent(IGESEvent @event)
        {
            return @event.EventType == "RegisterUser";
        }

        public ActionBlock<IGESEvent> ReturnActionBlock()
        {
            return new ActionBlock<IGESEvent>(x =>
            {
                if (ExpectEventPositionIsGreaterThanLastRecorded(x)) { return; };
                
                var registerUser = (RegisterUser)x;
                var user = new User(true);
                user.Handle(registerUser);
                _getEventStoreRepository.Save(user, Guid.NewGuid());
                // noise
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Command Saved: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(JsonConvert.SerializeObject(user));
                Console.Write(Environment.NewLine);
                // noise
                SetEventAsRecorded(x);
            });
        }
    }
}