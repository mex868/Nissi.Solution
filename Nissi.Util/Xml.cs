using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Nissi.Util
{
    public static class Xml
    {
        public static string ToXml(this object objeto)
        {
            var xml = new StringWriter();
            var xmlSerializer = new XmlSerializer(objeto.GetType());
            xmlSerializer.Serialize(xml, objeto);
            return xml.ToString();
        }
    }
}
