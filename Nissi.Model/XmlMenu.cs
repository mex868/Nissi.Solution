using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Nissi.Model
{

    [XmlRoot(ElementName = "menu")]
    public class XmlMenu
    {
        [XmlElement(ElementName = "menuItem")]    
        public List<XmlMenuItem> XmlMenuItem { get; set; }
    }
}
