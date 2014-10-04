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
    public class LoginUserWorkflow : HandlerBase, IHandler
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;
        public LoginUserWorkflow(IMongoRepository mongoRepository, IGetEventStoreRepository getEventStoreRepository)
            : base(mongoRepository)
        {
            _repository = getEventStoreRepository;
            _getEventStoreRepository = getEventStoreRepository;
            register(typeof(LoginUser), loginUser);
        }

        private async Task<User> loginUser(IGESEvent x)
        {
            var loginUser = (LoginUser)x;
            User user = await _getEventStoreRepository.GetById<User>(loginUser.Id);
            user.Handle(loginUser);
            return user;
        }
    }
}