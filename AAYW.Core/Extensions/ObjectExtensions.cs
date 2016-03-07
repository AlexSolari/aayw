using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AAYW.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static string Serialize(this object obj)
        {
            var xml = string.Empty;
            XmlSerializer xsSubmit = new XmlSerializer(obj.GetType());
            using (StringWriter sww = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                xsSubmit.Serialize(writer, obj);
                xml = sww.ToString();
            }
            return xml;
        }

        public static TDest DeserializeAs<TDest>(this XmlDocument src)
        {
            var reader = new XmlNodeReader(src);
            XmlSerializer serializer = new XmlSerializer(typeof(TDest));
            return (TDest)serializer.Deserialize(reader);
        }
    }
}
