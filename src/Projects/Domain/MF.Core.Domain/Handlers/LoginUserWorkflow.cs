using System.Threading.Tasks;
using MF.Core.Domain.AggregateRoots;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Command;

namespace MF.Core.Domain.Handlers
{
    public class LoginUserWorkflow : WorkflowBase, IHandler
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;
        public LoginUserWorkflow(IMongoRepository mongoRepository, IGetEventStoreRepository getEventStoreRepository)
            : base(getEventStoreRepository, mongoRepository)
        {
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