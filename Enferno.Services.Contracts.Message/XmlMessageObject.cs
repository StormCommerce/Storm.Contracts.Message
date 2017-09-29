using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Enferno.Services.Contracts
{
    [Serializable]
    [XmlType(Namespace = "http://Enferno.Native.Schemas.XmlMessageObject")]
    [XmlRoot(Namespace = "http://Enferno.Native.Schemas.XmlMessageObject")]
    public class XmlMessageObject : Entity, IMessageObject
    {
        public static T FromString<T>(string xml) where T : IMessageObject
        {
            if (string.IsNullOrEmpty(xml)) return default(T);

            var serializer = new XmlSerializer(typeof(T));

            using(var sr = new StringReader(xml))
            using (var reader = new XmlTextReader(sr))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        public override string ToString()
        {
            var serializer = new XmlSerializer(GetType());

            var buff = new StringBuilder();
            using(var sw = new StringWriter(buff))
            using (var writer = new XmlTextWriter(sw))
            {
                serializer.Serialize(writer, this);
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
    }
}
