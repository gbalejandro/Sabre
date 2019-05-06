using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sabre.Models
{
    public class Credential
    {
        [XmlElement(ElementName = "userName")]
        public string userName { get; set; }
        [XmlElement(ElementName = "password")]
        public string password { get; set; }

        public Credential()
        {

        }

        public Credential(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }
    }
}