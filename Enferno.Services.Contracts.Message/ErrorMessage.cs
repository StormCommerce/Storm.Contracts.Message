using System;
using System.Runtime.Serialization;

namespace Enferno.Services.Contracts
{
    public interface IErrorMessage<T> where T : IMessageObject
    {
        int MessageId { get; set; }
        string Message { get; set; }
        string Orchestration { get; set; }
        DateTime TimeStamp { get; set; }
        T Record { get; set; }
    }

    [DataContract(Name = "ErrorMessage", Namespace = "http://Enferno.Native.Schemas.ErrorMessage")]
    public class ErrorMessage<T> : IErrorMessage<T>, IMessageObject, IExtensibleDataObject where T : IMessageObject
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
        public T Record { get; set; }

        public virtual ExtensionDataObject ExtensionData { get; set; }
        
        public ErrorMessage() 
        {
            TimeStamp = DateTime.Now;
        }

        public ErrorMessage(Exception ex) : this()
        {
            Message = ex.Message;
        }
    }
}
