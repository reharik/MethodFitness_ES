using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MF.Core.Infrastructure.GES.Interfaces
{
    public interface IGetEventStoreRepository 
    {
        Task<TAggregate> GetById<TAggregate>(Guid id) where TAggregate : class, IAggregate;
        Task<TAggregate> GetById<TAggregate>(Guid id, int version) where TAggregate : class, IAggregate;
        void Save(IAggregate aggregate, Guid commitId, string continuationId = null);
        void Save(IAggregate aggregate, Guid commitId, Dictionary<string, object> updateHeaders = null);
    }
}