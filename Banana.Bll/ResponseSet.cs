using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Banana.Entity;

namespace Banana.Bll
{
    [Serializable]
    [XmlRoot("response")]
    public class ResponseSet
    {
        public ResponseSet() { }

        public ResponseSet(Status status)
        {
            this.Status = status;
        }

        [XmlElement("status")]
        public Status Status { get; set; }
    }

    [Serializable]
    [XmlRoot("response")]
    public class ResponseSet<TEntity> : ISerializable, IXmlSerializable where TEntity : class, new()
    {
        private string bodyName = typeof(TEntity).Name[0].ToString().ToLower() + typeof(TEntity).Name.Substring(1);

        [XmlAttribute("status")]
        public Status Status { get; set; }

        public TEntity Entity { get; set; }

        public ResponseSet() { }

        protected ResponseSet(SerializationInfo info, StreamingContext context)
        {
            this.Status = info.GetValue("status", typeof(Status)) as Status;
            this.Entity = info.GetValue(bodyName, typeof(TEntity)) as TEntity;
        }

        public ResponseSet(Status status, TEntity entity)
        {
            Status = status;
            Entity = entity;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("status", this.Status);
            if (this.Entity != null)
            {
                info.AddValue(bodyName, this.Entity);
            }
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteStartElement("status");
            writer.WriteAttributeString("code", this.Status.Code.ToString());
            writer.WriteAttributeString("description", this.Status.Description);
            writer.WriteEndElement();
            if (this.Entity != null)
            {
                XmlSerializer xs = new XmlSerializer(this.Entity.GetType());
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                xs.Serialize(writer, this.Entity, ns);
            }
        }
    }
}
