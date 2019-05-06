using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace InterfaceOTA.Models
{
    public interface IReservationRepository
    {
        OTA_HotelResNotifRS Add(string request);
    }
}
