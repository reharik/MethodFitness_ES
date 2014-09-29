using System.Globalization;
using MF.Core.Infrastructure.GES.Exceptions;
using MF.Core.Infrastructure.GES.Interfaces;

namespace MF.Core.Infrastructure.GES
{
    internal static class ExtensionMethods
	{
		public static string FormatWith(this string format, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture, format ?? string.Empty, args);
		}

		public static void ThrowHandlerNotFound(this IAggregate aggregate, object eventMessage)
		{
			var exceptionMessage = "Aggregate of type '{0}' raised an event of type '{1}' but not handler could be found to handle the message."
				.FormatWith(aggregate.GetType().Name, eventMessage.GetType().Name);

			throw new HandlerForDomainEventNotFoundException(exceptionMessage);
		}
	}
}