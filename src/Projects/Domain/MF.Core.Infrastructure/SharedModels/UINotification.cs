using EventStore.ClientAPI;

namespace MF.Core.Infrastructure.SharedModels
{
    public class UINotification : GESEvent
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