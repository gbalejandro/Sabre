using Newtonsoft.Json;
using Sabre.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Text.RegularExpressions;

namespace Sabre.Controllers
{
    public class ReservationsController : ApiController
    {
 
        // GET: api/Reservations
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Reservations/5
        public string Get(int id)
        {
            return "value";
        }
       
        // POST: api/Reservations
        public HttpResponseMessage Post(HttpRequestMessage request)
        {
            String input = "";
            string response = "", numeroBooking = "";
            Serializer ser = new Serializer();

            var doc = new XmlDocument();
            doc.Load(request.Content.ReadAsStreamAsync().Result);

            XmlNodeList nodeList = doc.GetElementsByTagName("HotelReservationID");

            foreach (XmlNode node in nodeList)
            {
                if (node.Attributes.GetNamedItem("ResID_Source").Value == "BookingChannel")
                {
                    numeroBooking = node.Attributes.GetNamedItem("ResID_Value").Value;
                }
            }
            
            File.WriteAllText(@"C:\ASP\WS\InterfaceOTA\documents\" + numeroBooking + ".xml", doc.InnerXml);

            XmlNodeList elemList = doc.GetElementsByTagName("soapenv:Body");
            input = elemList[0].InnerXml;

            XmlNodeList nodeAgencia = doc.GetElementsByTagName("CompanyName");

            try
            {
                if (nodeAgencia[0].InnerText == "BestDay" || nodeAgencia[0].InnerText == "PriceTravel")
                {
                    OTA_HotelResNotifRQ soap = ser.Deserialize<OTA_HotelResNotifRQ>(input);
                    ReservationRepository reservation = new ReservationRepository();

                    response = reservation.ProcesaReservacion(soap);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, new HttpError("This Channel isn't available"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(response, Encoding.UTF8, "text/xml");
            return res;
        }

        private string regresaMensaje(string mensaje)
        {
            string soap = "";
            soap += "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:con=\"http://connectors.omnibees.com/\" xmlns =\"http://www.opentravel.org/OTA/2003/05\" > ";
            soap += "<soapenv:Body>";
            soap += "<Warnings />";
            soap += "<Errors>";
            soap += "<Error>" + mensaje + "</Error>";
            soap += "</Errors>";
            soap += "</OTA_HotelResNotifRS>";
            soap += "</soapenv:Body>";
            soap += "</soapenv:Envelope>";
            return soap;
        }

        // PUT: api/Reservations/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Reservations/5
        public void Delete(int id)
        {
        }
    }
}
