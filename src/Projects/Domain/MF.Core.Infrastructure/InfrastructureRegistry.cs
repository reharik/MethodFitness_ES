using EventStore.ClientAPI;
using MF.Core.Infrastructure.GES;
using MF.Core.Infrastructure.GES.Interfaces;
using MF.Core.Infrastructure.Mongo;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace MF.Core.Infrastructure
{
     public class InfrastructureRegistry : Registry
    {
        public InfrastructureRegistry()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
            });
            For<ILogger>().AlwaysUnique().Use(s => s.ParentType == null ? new ConsoleLogger(s.RootType) : new ConsoleLogger(s.ParentType));
            For<IMongoDB>().Use(x => new Mongo.MongoDB("mongodb://localhost"));
            For<IMongoRepository>().Use(x => new MongoRepository(x.GetInstance<IMongoDB>().GetDatabase()));
            For<IEventStoreConnection>().Use(x => x.GetInstance<IGESConnection>().BuildConnection());
            For<IGetEventStoreRepository>().Use(x => new GetEventStoreRepository(x.GetInstance<IEventStoreConnection>()));
        }
    }
}