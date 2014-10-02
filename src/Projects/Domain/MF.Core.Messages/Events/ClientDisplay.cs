using System;

namespace MF.Core.Messages.Events
{
    public class ClientDisplay
    {
        public ClientDisplay(Guid clientId, string clientName)
        {
            ClientId = clientId;
            ClientName = clientName;
        }

        public Guid ClientId { get; set; }
        public string ClientName { get; private set; }
    }
}