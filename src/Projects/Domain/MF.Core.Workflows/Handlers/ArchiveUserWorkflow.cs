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
    public class ArchiveUserWorkflow : HandlerBase, IHandler
    {
        private readonly IGetEventStoreRepository _getEventStoreRepository;

        public ArchiveUserWorkflow(IMongoRepository mongoRepository, IGetEventStoreRepository getEventStoreRepository)
            : base(mongoRepository)
        {
            _repository = getEventStoreRepository;
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