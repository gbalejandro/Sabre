using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Sabre.Models
{
    public class MapeoHoteles
    {
        private string hotel_ob;
        private string desc_ob;
        private string hotel_os;
        private string hotel_un;
        private string hotel_pw;
        private string hotel_cn;
        private string hotel_siglas;
        private string hotel_fase;
        private string hotel_ip;

        public string Hotel_ob
        {
            get
            {
                return hotel_ob;
            }

            set
            {
                hotel_ob = value;
            }
        }

        public string Desc_ob
        {
            get
            {
                return desc_ob;
            }

            set
            {
                desc_ob = value;
            }
        }

        public string Hotel_os
        {
            get
            {
                return hotel_os;
            }

            set
            {
                hotel_os = value;
            }
        }

        public string Hotel_un
        {
            get
            {
                return hotel_un;
            }

            set
            {
                hotel_un = value;
            }
        }

        public string Hotel_pw
        {
            get
            {
                return hotel_pw;
            }

            set
            {
                hotel_pw = value;
            }
        }

        public string Hotel_cn
        {
            get
            {
                return hotel_cn;
            }

            set
            {
                hotel_cn = value;
            }
        }

        public string Hotel_siglas
        {
            get
            {
                return hotel_siglas;
            }

            set
            {
                hotel_siglas = value;
            }
        }

        public string Hotel_fase
        {
            get
            {
                return hotel_fase;
            }

            set
            {
                hotel_fase = value;
            }
        }

        public string Hotel_ip
        {
            get
            {
                return hotel_ip;
            }

            set
            {
                hotel_ip = value;
            }
        }

        public MapeoHoteles ObtenerParamsBDHotel(string IdHotelOB)
        {
            string StringConexion = ConfigurationManager.ConnectionStrings["PegasoConn"].ToString();
            OracleConnection con = new OracleConnection();
            con.ConnectionString = StringConexion;
            OracleCommand cmd = new OracleCommand();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "select * from obhotelmap where oh_hotel_ob = '" + IdHotelOB + "'";
            OracleDataReader reader = cmd.ExecuteReader();
            MapeoHoteles mh = new MapeoHoteles();
            while (reader.Read())
            {
                mh.Desc_ob = Convert.ToString(reader["OH_DESC_OB"]);
                mh.Hotel_os = Convert.ToString(reader["OH_HOTEL_OS"]);
                mh.Hotel_un = Convert.ToString(reader["OH_HOTEL_UN"]);
                mh.Hotel_pw = Convert.ToString(reader["OH_HOTEL_PW"]);
                mh.Hotel_cn = Convert.ToString(reader["OH_HOTEL_CN"]);
                mh.Hotel_siglas = Convert.ToString(reader["OH_HOTEL_SIGLAS"]);
                mh.Hotel_fase = Convert.ToString(reader["OH_HOTEL_FASE"]);
                mh.Hotel_ip = Convert.ToString(reader["OH_HOTEL_IP"]);
            }

            reader.Dispose();
            cmd.Dispose();
            con.Dispose();

            return mh;
        }
    }
}