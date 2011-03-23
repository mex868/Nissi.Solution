using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Nissi.Model
{

    [XmlRoot(ElementName = "menuItem")]
    public class XmlMenuItem
    {
        [XmlElement(ElementName = "text")]
        public string Text { get; set; }

        [XmlElement(ElementName = "subMenu")]
        public XmlSubMenu XmlSubMenu { get; set; }

    }
}
