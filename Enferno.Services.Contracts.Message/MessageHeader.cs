using System;
using System.Runtime.Serialization;

namespace Enferno.Services.Contracts
{
    public interface IMessageHeader
    {
        Guid MessageId { get; set; }
        Guid? Account { get; set; }
        int? ClientId { get; set; }
        Guid? Application { get; set; }
        string GatewayKey { get; set; }
        int? ValidationId { get; set; }
        DateTime Timestamp { get; set; }
        DateTime? ValidTo { get; set; }
        int? AccountId { get; set; }

        void CopyCommonValues(MessageHeader header);
    }

    [DataContract(Name = "MessageHeader", Namespace = "http://Enferno.Native.Schemas.MessageHeader")]
    public class MessageHeader : IMessageHeader, IExtensibleDataObject
    {
        [DataMember(Order = 1)]
        public Guid MessageId { get; set; }
        [DataMember(Order = 2)]
        public Guid? Account { get; set; }
        [DataMember(Order = 3)]
        public int? ClientId { get; set; }
        [DataMember(Order = 4)]
        public Guid? Application { get; set; }
        [DataMember(Order = 5)]
        public string GatewayKey { get; set; }
        [DataMember(Order = 6)]
        public int? ValidationId { get; set; }
        [DataMember(Order = 7)]
        public DateTime Timestamp { get ; set; }
        [DataMember(Order = 8)]
        public DateTime? ValidTo { get; set; }
        [DataMember(Order = 9)]
        public int? AccountId { get; set; }

        public virtual ExtensionDataObject ExtensionData { get; set; }

        public void CopyCommonValues(MessageHeader header)
        {
            Account = header.Account;
            Application = header.Application;
            ClientId = header.ClientId;
            GatewayKey = header.GatewayKey;
        }
    }
}
