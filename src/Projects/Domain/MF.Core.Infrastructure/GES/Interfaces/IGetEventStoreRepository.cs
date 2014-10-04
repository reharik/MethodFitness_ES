using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MF.Core.Infrastructure.GES.Interfaces
{
    public interface IRepository
    {
        void Save<T>(T item);
    }

    public interface IGetEventStoreRepository : IRepository
    {
        Task<TAggregate> GetById<TAggregate>(Guid id) where TAggregate : class, IAggregate;
        Task<TAggregate> GetById<TAggregate>(Guid id, int version) where TAggregate : class, IAggregate;
    }
}