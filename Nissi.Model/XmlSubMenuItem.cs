using System;
using System.Xml.Serialization;

namespace Nissi.Model
{

    [XmlRoot(ElementName = "menuItem")]
    public class XmlSubMenuItem
    {
        [XmlElement(ElementName = "text")]
        public string Text { get; set; }
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
        [XmlElement(ElementName = "resolveurl")]
        public bool ResolveUrl { get; set; }
    }
}
