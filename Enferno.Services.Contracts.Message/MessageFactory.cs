using System;

namespace Enferno.Services.Contracts
{
    public abstract class MessageFactory
    {
        public static MessageHeader CreateHeader()
        {
            var message = new MessageHeader
            {
                MessageId = Guid.NewGuid(),
                Timestamp = DateTime.Now
            };
            return message;
        }
    }
}
