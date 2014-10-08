using System.Threading.Tasks;
using MF.Core.Domain.AggregateRoots;
using MF.Core.Infrastructure;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.Mongo;
using MF.Core.Infrastructure.SharedModels;
using MF.Core.Messages.Command;

namespace MF.Core.Domain.Handlers
{
    public class UnArchiveUserWorkflow : WorkflowBase, IHandler
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;
        public UnArchiveUserWorkflow(IMongoRepository mongoRepository, IGetEventStoreRepository getEventStoreRepository)
            : base(getEventStoreRepository, mongoRepository)
        {
            _getEventStoreRepository = getEventStoreRepository;
            register(typeof(UnArchiveUser), unArchiveUser);
        }

        private async Task<User> unArchiveUser(IGESEvent x)
        {
            var vent = (UnArchiveUser)x;
            var item = await _getEventStoreRepository.GetById<User>(vent.TrainerId);
            item.Handle(vent);
            return item;
        }
    }
}