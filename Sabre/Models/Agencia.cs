using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Sabre.Models
{
    public class Agencia
    {
        public string ag_oasis { get; set; }
        public string plan { get; set; }
        public string ag_omni { get; set; }
        public string moneda { get; set; }
        public string fpago { get; set; }
        public string mayorista { get; set; }

        public Agencia ObtenerCodigoAgencia(ReservationModel rv)
        {
            string StringConexion = ConfigurationManager.ConnectionStrings["PegasoConn"].ToString();
            OracleConnection con = new OracleConnection();
            con.ConnectionString = StringConexion;
            OracleCommand cmd = new OracleCommand();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "select ag_oasis, mayorista from OBAGENCIA where ag_omni = '" + rv.rva_chanelname_obees.ToUpper() + "' and PLAN = '" + rv.rva_meal_plancode
                + "' and moneda = '" + rv.rva_moneda + "'";
            OracleDataReader reader = cmd.ExecuteReader();
            Agencia ag = new Agencia();
            while (reader.Read())
            {
                //ag.ag_oasis = reader["ag_oasis"].ToString();
                ag.mayorista = reader["mayorista"].ToString();
            }

            reader.Dispose();
            cmd.Dispose();
            con.Dispose();

            return ag;
        }
    }
}