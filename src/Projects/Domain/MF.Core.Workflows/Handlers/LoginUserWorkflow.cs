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
    public class LoginUserWorkflow : HandlerBase, IHandler
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;

        public LoginUserWorkflow(IGetEventStoreRepository getEventStoreRepository, IMongoRepository mongoRepository)
            : base(mongoRepository)
        {
            _getEventStoreRepository = getEventStoreRepository;
            _handlerType = "LoginUserWorkflow";
        }

        public bool HandlesEvent(IGESEvent @event)
        {
            return @event.EventType == "LoginUser";
        }

        public ActionBlock<IGESEvent> ReturnActionBlock()
        {
            return new ActionBlock<IGESEvent>(async x =>
                {
                    if (ExpectEventPositionIsGreaterThanLastRecorded(x)) { return; }

                    var loginUser = (LoginUser)x;
                    User user = await _getEventStoreRepository.GetById<User>(loginUser.Id);
                    user.Handle(loginUser);
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