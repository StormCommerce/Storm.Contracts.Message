using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;

namespace Enferno.Services.Contracts
{
    [CollectionDataContract
    (Name = "Messages",
    ItemName = "Message",
    KeyName = "Key",
    ValueName = "Value",
    Namespace = "http://Enferno.Native.Schemas.ErrorMessage_v2")]
    public class MessagesDictionary : Dictionary<string, string> { }

    [DataContract(Name = "ErrorMessage", Namespace = "http://Enferno.Native.Schemas.ErrorMessage_v2")]
    public class ErrorMessage_v2 : IExtensibleDataObject
    {
        [DataMember(Name = "MessageId", Order = 1)]
        public int MessageId { get; set; }
        [DataMember(Name = "Message", Order = 2)]
        public string Message { get; set; }
        [DataMember(Name = "Orchestration", Order = 3)]
        public string Orchestration { get; set; }
        [DataMember(Name = "TimeStamp", Order = 4)]
        public DateTime TimeStamp { get; set; }
        [DataMember(Name = "Record", Order = 5)]
        public XmlElement Record { get; set; }
        [DataMember(Name = "Messages", Order = 6)]
        public MessagesDictionary Messages { get; set; }

        public virtual ExtensionDataObject ExtensionData { get; set; }

        public ErrorMessage_v2()
        {
            TimeStamp = DateTime.Now;
            Messages = new MessagesDictionary();
        }

        public ErrorMessage_v2(Exception ex) : this(ex.Message) { }

        public ErrorMessage_v2(string message): this()
        {
            Message = string.IsNullOrWhiteSpace(message) || message.Length <= 200 ? message : message.Substring(0,200);
        }

        public ErrorMessage_v2(XmlElement record, string message) : this(message)
        {
            Record = record;
        }
    }
}
