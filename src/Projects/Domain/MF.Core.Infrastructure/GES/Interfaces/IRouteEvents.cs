using System;

namespace MF.Core.Infrastructure.GES.Interfaces
{
    public interface IRouteEvents
	{
		void Register<T>(Action<T> handler);
		void Register(IAggregate aggregate);

		void Dispatch(object eventMessage);
	}
}