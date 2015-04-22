using System;
using System.Collections;

namespace MF.Core.Infrastructure.GES.Interfaces
{
    public interface IAggregate
	{
		Guid Id { get; }
		int Version { get; }

		void ApplyEvent(object @event);
		ICollection GetUncommittedEvents();
		void ClearUncommittedEvents();

	}
}