using EventStore.ClientAPI;
using MF.Core.Infrastructure.SharedModels;

namespace MF.Core.Messages
{
    public class UINotification : IGESEvent
    {
        public UINotification(string notificationType, string message)
        {
            NotificationType = notificationType;
            Message = message;
            EventType = GetType().Name;
        }

        public string NotificationType { get; private set; }
        public string Message { get; private set; }
        public string EventType { get; private set; }
        public Position? OriginalPosition { get; set; }
    }
}