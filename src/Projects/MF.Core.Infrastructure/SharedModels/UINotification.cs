using EventStore.ClientAPI;

namespace MF.Core.Infrastructure.SharedModels
{
    public class UINotification : GESEvent
    {
        public UINotification(string notificationType, string message, string initialEvent)
        {
            NotificationType = notificationType;
            Message = message;
            InitialEvent = initialEvent;
            EventType = GetType().Name;
        }

        public string NotificationType { get; private set; }
        public string Message { get; set; }
        public string InitialEvent { get; private set; }
    }
}