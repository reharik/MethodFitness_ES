using System.Threading.Tasks;
using MF.Core.Domain.AggregateRoots;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Command;

namespace MF.Core.Domain.Handlers
{
    public class ArchiveUserWorkflow : WorkflowBase, IHandler
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;

        public ArchiveUserWorkflow(IMongoRepository mongoRepository, IGetEventStoreRepository getEventStoreRepository)
            : base(getEventStoreRepository, mongoRepository)
        {
            _getEventStoreRepository = getEventStoreRepository;
            register(typeof(ArchiveUser), archiveUser);
        }

        private async Task<User> archiveUser(IGESEvent x)
        {
            var archiveUser = (ArchiveUser)x;
            User user = await _getEventStoreRepository.GetById<User>(archiveUser.TrainerId);
            user.Handle(archiveUser);
            return user;
        }
    }
}