using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nissi.Model
{

    [XmlRoot(ElementName = "subMenu")]
    public class XmlSubMenu
    {
        [XmlElement(ElementName = "menuItem")]
        public List<XmlSubMenuItem> XmlSubMenuItem { get; set; }

    }
}
