using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Enferno.Services.Contracts
{
    public interface IMessageObject
    {
        /*Just a marker*/
    }

    [DataContract(Name = "MessageObject", Namespace = "http://Enferno.Native.Schemas.MessageObject")]
    [Serializable]
    public class MessageObject : IMessageObject
    {
        public override string ToString()
        {
            var serializer = new DataContractSerializer(GetType());

            var buff = new StringBuilder();
            using(var sw = new StringWriter(buff))
            using (var writer = new XmlTextWriter(sw))
            {
                serializer.WriteObject(writer, this);
                return buff.ToString();
            }
        }

        public XmlElement ToXml()
        {
            var document = new XmlDocument();
            document.LoadXml(ToString());
            return document.DocumentElement;
        }

        public XElement ToXElement()
        {
            var doc = XDocument.Parse(ToString());
            return doc.Root;
        }

        public static T FromString<T>(string xml) where T : IMessageObject
        {
            if (string.IsNullOrEmpty(xml)) return default(T);

            var serializer = new DataContractSerializer(typeof(T));

            using(var sr = new StringReader(xml))
            using (var reader = new XmlTextReader(sr))
            {
                return (T)serializer.ReadObject(reader, false);
            }
        }
    }
}
