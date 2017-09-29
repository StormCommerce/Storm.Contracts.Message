using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Enferno.Services.Contracts
{
    public interface IEntity : IExtensibleDataObject
    {
        /*Just a marker*/
    }

    [DataContract(Name = "Entity", Namespace = "Enferno.Services.Contracts.Message")]
    public class Entity : IEntity
    {
        public virtual ExtensionDataObject ExtensionData { get; set; }

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

        public XElement ToXml()
        {
            var doc = XDocument.Parse(ToString());
            return doc.Root;
        }

        public static T FromString<T>(string xml) where T : IEntity
        {
            if (string.IsNullOrEmpty(xml)) return default(T);

            var serializer = new DataContractSerializer(typeof(T));

            using(var sr = new StringReader(xml))
            using (var reader = new XmlTextReader(sr))
            {
                return (T)serializer.ReadObject(reader, false);
            }
        }

        public T Clone<T>() where T : IEntity
        {
            return FromString<T>(ToString());
        }

        public bool TryGetExtensionDataMemberValue<T>(string dataMemberName, out T retval)
        {
            retval = default(T);
            if (ExtensionData == null) return false; 

            var membersProperty = typeof(ExtensionDataObject).GetProperty("Members", BindingFlags.NonPublic | BindingFlags.Instance);            
            var members = (IList)membersProperty.GetValue(ExtensionData, null);
            if (members == null) return false;

            foreach (var member in members)
            {
                var nameProperty = member.GetType().GetProperty("Name");
                var name = (string)nameProperty.GetValue(member, null);
                if (name == dataMemberName)
                {
                    var valueProperty = member.GetType().GetProperty("Value");
                    var value = valueProperty.GetValue(member, null);
                    var innerValueProperty = value.GetType().GetProperty("Value");
                    var innerValue = innerValueProperty.GetValue(value, null);
                    retval = innerValue != null ? (T)innerValue : default(T);
                    return true;
                }
            }

            return false;
        }
    }
}
