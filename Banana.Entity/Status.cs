using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Banana.Entity
{
    [Serializable]
    public class Status
    {
        [XmlAttribute("code")]
        public int Code { get; set; }

        [XmlAttribute("description")]
        public string Description { get; set; }
    }
}
