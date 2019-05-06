using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Sabre.Models
{
    public class ReservationModel
    {
        public string rva_echotoken { get; set; }
        public string rva_action { get; set; }
        public string rva_ResID_Type { get; set; }
        public string rva_ResID_Source { get; set; }
        public string rva_uniqueID { get; set; }
        public string rva_agen { get; set; }
        public string rva_notas { get; set; }
        public string rva_llegada { get; set; }
        public string rva_salida { get; set; }
        public int rva_noches { get; set; }
        public string rva_tipo_hab { get; set; }
        public string rva_tarifa { get; set; }
        public int rva_adulto { get; set; }
        public int rva_menor { get; set; }
        public string rva_hab_renta { get; set; }
        public string rva_hotel_renta { get; set; }
        public string rva_moneda { get; set; }
        public string rva_nombre { get; set; }
        public string rva_apell { get; set; }
        public string rva_plan { get; set; }
        public string rva_grupo { get; set; }
        public string rva_error { get; set; }
        public string rva_exito { get; set; }
        public string rva_oasis_rva { get; set; }
        public string rva_sec { get; set; }
        public string rva_email { get; set; }
        public string rva_chaincode { get; set; }
        public string rva_hotelcode { get; set; }
        public string rva_oasis_errcode { get; set; }
        public string rva_oasis_errdesc { get; set; }
        public string rva_servicio { get; set; }
        public string rva_companycode_obees { get; set; }
        public string rva_chanelname_obees { get; set; }
        public string rva_agencia_obees { get; set; }
        public string rva_codecontext_obees { get; set; }
        public bool rva_planindicador { get; set; }
        public string rva_plancode { get; set; }
        public string rva_mealplan { get; set; }
        public string rva_pais { get; set; }
        public string rva_tipo_huesped { get; set; }
        public string rva_importe { get; set; }
        public string rva_create_datetime { get; set; }
        public string rva_tipo_garantia { get; set; }
        public string rva_serv_price_type { get; set; }
        public double rva_tc { get; set; }
        public int rva_junior { get; set; }
        public int rva_bebe { get; set; }
        public string rva_reserva_omnibees { get; set; }
        public string rva_reserva_bookingchanel { get; set; }
        public string rva_servicerph { get; set; }
        public string rva_inclusive { get; set; }
        public string rva_quantity { get; set; }
        public string rva_idcontext_ob { get; set; }
        public string rva_codhab { get; set; }
        public string rva_numunits { get; set; }
        public string rva_diafecha { get; set; }
        public string rva_montodia { get; set; }
        public string rva_gcperroom { get; set; }
        public string rva_rateplanc { get; set; }
        public string rva_meal_plancode { get; set; }
        public string rva_deadline_time { get; set; }
        public string rva_deadline_multiplier { get; set; }
        public string rva_desc_garantia { get; set; }
        public string rva_resguest_indice { get; set; }
        public string rva_resguest_primary { get; set; }
        public string rva_resguest_arrivaltime { get; set; }
        public string rva_uniqueid_type { get; set; }
        public string rva_uniqueid_id { get; set; }
        public string rva_uniqueid_idcont { get; set; }
        public string rva_nameprefix { get; set; }
        public string rva_phone_techtype { get; set; }
        public string rva_phone_usetype { get; set; }
        public string rva_phonenumber { get; set; }
        public string rva_address1 { get; set; }
        public string rva_address2 { get; set; }
        public string rva_city_name { get; set; }
        public string rva_postal_code { get; set; }
        public string rva_doc_info { get; set; }
        public string rva_globalinf_perroom { get; set; }
        public double rva_tot_amount_aft_tax { get; set; }
        public double rva_tot_amount_bef_tax { get; set; }
        public string rva_state_prov { get; set; }
        public string rva_oaction { get; set; }
        public bool rva_have_error { get; set; }
        public string rva_error_type { get; set; }
        public string rva_error_code { get; set; }
        public string rva_cod_hotel_ob { get; set; }
        public string rva_mayorista { get; set; }
        public string rva_origen { get; set; }
        public string rva_status { get; set; }
        public string rva_fase { get; set; }
        public string rva_err_recordid { get; set; }
        public string rva_err_value { get; set; }
        public string rva_cap_u { get; set; }

        public ReservationModel()
        {

        }

        public bool interfazarReservasHotel(MapeoHoteles mh, ref ReservationModel model)
        {
            bool ok = false;
            // obtengo la reservacion de la tabla temporal
            string StringConexion = ConfigurationManager.ConnectionStrings["PegasoConn"].ToString();
            //StringConexion = "User Id=" + mh.Hotel_un + ";Password=" + mh.Hotel_pw + "; Data Source=" + mh.Hotel_ip + ":1521/" + mh.Hotel_cn + "; Pooling=false";
            OracleConnection con = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            
            try
            {

                con.ConnectionString = StringConexion;

                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "select * from obreserva where rv_reserva = '" + model.rva_oasis_rva + "' and rv_hotel_renta = " +
                    "'" + model.rva_hotel_renta + "' and rv_action = '"+ model.rva_oaction + "'";
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //model.rva_oasis_rva = reader["RV_RESERVA"].ToString();
                    model.rva_mayorista = reader["RV_MAYORISTA"].ToString();
                    model.rva_agen = reader["RV_AGENCIA"].ToString();
                    model.rva_pais = reader["RV_PAIS"].ToString();
                    model.rva_address1 = reader["RV_DIR1"].ToString();
                    model.rva_address2 = reader["RV_DIR2"].ToString();
                    model.rva_tipo_huesped = reader["RV_TIPO_HUESPED"].ToString();
                    model.rva_origen = reader["RV_ORIGEN"].ToString();
                    model.rva_notas = reader["RV_NOTAS"].ToString();
                    model.rva_llegada = reader["RV_LLEGADA"].ToString();
                    model.rva_salida = reader["RV_SALIDA"].ToString();
                    model.rva_noches = Convert.ToInt32(reader["RV_NOCHES"].ToString());
                    model.rva_tipo_hab = reader["RV_TIPO_HAB"].ToString();
                    model.rva_tarifa = reader["RV_TARIFA"].ToString();
                    model.rva_adulto = Convert.ToInt32(reader["RV_ADULTO"].ToString());
                    model.rva_menor = Convert.ToInt32(reader["RV_MENOR"].ToString());
                    model.rva_fase = reader["RV_FASE"].ToString();
                    model.rva_reserva_bookingchanel = reader["RV_VOUCHER"].ToString();
                    model.rva_importe = reader["RV_IMPORTE"].ToString();
                    //model.rva_status = reader["RV_ACTION"].ToString();
                    model.rva_meal_plancode = reader["RV_PLANES"].ToString();
                    model.rva_hab_renta = reader["RV_TIPO_HAB_RENTA"].ToString();
                    model.rva_hotel_renta = reader["RV_HOTEL_RENTA"].ToString();
                    model.rva_email = reader["RV_EMAIL"].ToString();
                    model.rva_moneda = reader["RV_MONEDA"].ToString();
                    model.rva_tc = Convert.ToDouble(reader["RV_TC"].ToString());
                }
                ok = procesarReserva(mh, ref model);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                cmd.Dispose();
                con.Dispose();
            }
            return ok;
        }

        private bool procesarReserva(MapeoHoteles mh, ref ReservationModel rm)
        {
            bool ok = false;
            string stringConexion = "User Id=" + mh.Hotel_un + ";Password=" + mh.Hotel_pw + ";Data Source=" + mh.Hotel_ip + ":1521/" + mh.Hotel_cn + ";Pooling=false";
            //string stringConexion = "User Id=frgrand;Password=service;Data Source=192.168.0.9:1521/pegaso;Pooling=false";
            DateTime Hoy = DateTime.Today;
            string fecha_actual = Hoy.ToString();//Hoy.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));
            fecha_actual = fecha_actual.Substring(0, 10);
            string hora_actual = DateTime.Now.ToString("HH:mm");
            OracleConnection con = new OracleConnection();
            con.ConnectionString = stringConexion;
            OracleCommand cmd = new OracleCommand();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            string dir1 = "", dir2 = "", fllegada = "", fsalida = "";
            if (!string.IsNullOrEmpty(rm.rva_address1))
            {
                if (rm.rva_address1.Length > 50)
                {
                    dir1 = rm.rva_address1.Substring(0, 50);
                }
                else
                {
                    dir1 = rm.rva_address1;
                }
            }
            if (!string.IsNullOrEmpty(rm.rva_address2))
            {
                if (rm.rva_address2.Length > 50)
                {
                    dir2 = rm.rva_address2.Substring(0, 50);
                }
                else
                {
                    dir2 = rm.rva_address2;
                }
            }
            DateTime dLlegada = Convert.ToDateTime(rm.rva_llegada);
            fllegada = dLlegada.ToString("dd/MM/yyyy");
            DateTime dSalida = Convert.ToDateTime(rm.rva_salida);
            fsalida = dSalida.ToString("dd/MM/yyyy");
            int rowsInsert = 0;

            // verifico el tipo de operacion
            switch (rm.rva_oaction)
            {
                case "B":
                    try
                    {
                        string sql = "insert into freserva (rv_mayorista, rv_agencia, rv_pais, rv_tipo_huesped, rv_origen, rv_procede, " +
                            "rv_notas, rv_llegada, rv_salida, rv_noches, rv_llegada_h, rv_salida_h, rv_tipo_hab, rv_tarifa, rv_adulto, rv_menor, " +
                            "rv_bebe, rv_fase, rv_voucher, rv_importe, rv_deposito, rv_cap_f, rv_cap_h, rv_cap_u, rv_status, rv_planes, " +
                            "rv_tipo_hab_renta, rv_hotel_renta, rv_email, rv_moneda, rv_tipo_venta, rv_dir1, rv_dir2, rv_reserva, rv_junior,rv_tc) values " +
                            "('" + rm.rva_mayorista + "', '" + rm.rva_agen + "', '" + rm.rva_pais + "', '" + rm.rva_tipo_huesped + "', '" + rm.rva_origen + "', " +
                            "'" + rm.rva_mayorista + "', '" + rm.rva_notas + "', '"+ fllegada + "', " +
                            "'"+ fsalida + "', " + rm.rva_noches + ", '15:00', '12:00', '" + rm.rva_tipo_hab + "', " +
                            "'" + rm.rva_tarifa + "', " + rm.rva_adulto + ", " + rm.rva_menor + ", 0, '" + rm.rva_fase + "', " +
                            "'" + rm.rva_reserva_bookingchanel + "', " + Convert.ToDouble(rm.rva_importe).ToString(CultureInfo.InvariantCulture) + ", " +
                            "3, '" + fecha_actual + "', '" + hora_actual + "', 'OMBEES', 'R', '" + rm.rva_meal_plancode + "', " +
                            "'" + rm.rva_hab_renta + "', '" + rm.rva_hotel_renta + "', '" + rm.rva_email + "', '" + rm.rva_moneda + "', 'ONLINE', " +
                            "'" + dir1 + "', '" + dir2 + "', '" + rm.rva_oasis_rva + "', 0, " + Convert.ToDouble(rm.rva_tc).ToString(CultureInfo.InvariantCulture) + ")";
                        cmd.CommandText = sql;
                        rowsInsert = cmd.ExecuteNonQuery();
                    } catch (Exception ex)
                    {

                    }
                    finally
                    {
                        con.Close();
                        cmd.Dispose();
                        con.Dispose();
                    }

                    if (rowsInsert > 0)
                    {
                        ok = true;
                    }
                    else
                    {
                        //rm.rva_error_type = "ApplicationError";
                        //rm.rva_error_code = "SystemError";
                        //rm.rva_oasis_errdesc = "InternalError";
                        rm.rva_err_recordid = rm.rva_reserva_bookingchanel;
                        rm.rva_err_value = "NO SE PUDO INSERTAR LA RESERVA EN EL HOTEL";
                    }
                    break;
                case "M":
                    // primero verifico que exista la reservacion en status R
                    string sql1 = "select * from freserva where rv_reserva = '"+rm.rva_oasis_rva + "' and rv_hotel_renta = '"+ rm.rva_hotel_renta + "'";
                    OracleCommand cmd2 = new OracleCommand();
                    string campo = "";

                    try
                    {
                        cmd2 = con.CreateCommand();
                        cmd2.CommandText = sql1;
                        OracleDataReader reader2 = cmd2.ExecuteReader();

                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {
                                if (!rm.rva_mayorista.Equals(reader2["RV_MAYORISTA"].ToString()))
                                {
                                    campo = reader2.GetName(1);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_MAYORISTA"].ToString(), rm.rva_mayorista, mh);
                                }
                                if (!rm.rva_agen.Equals(reader2["RV_AGENCIA"].ToString()))
                                {
                                    campo = reader2.GetName(2);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_AGENCIA"].ToString(), rm.rva_agen, mh);
                                }
                                if (!rm.rva_pais.Equals(reader2["RV_PAIS"].ToString()))
                                {
                                    campo = reader2.GetName(4);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_PAIS"].ToString(), rm.rva_pais, mh);
                                }
                                if (!rm.rva_address1.Equals(reader2["RV_DIR1"].ToString()))
                                {
                                    campo = reader2.GetName(6);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_DIR1"].ToString(), rm.rva_address1, mh);
                                }
                                if (!rm.rva_address2.Equals(reader2["RV_DIR2"].ToString()))
                                {
                                    campo = reader2.GetName(7);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_DIR2"].ToString(), rm.rva_address2, mh);
                                }
                                if (!rm.rva_tipo_huesped.Equals(reader2["RV_TIPO_HUESPED"].ToString()))
                                {
                                    campo = reader2.GetName(8);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_TIPO_HUESPED"].ToString(), rm.rva_tipo_huesped, mh);
                                }
                                if (!rm.rva_origen.Equals(reader2["RV_ORIGEN"].ToString()))
                                {
                                    campo = reader2.GetName(10);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_ORIGEN"].ToString(), rm.rva_origen, mh);
                                }
                                //rm.rva_notas = reader2["RV_NOTAS"].ToString();
                                if (!rm.rva_llegada.Equals(reader2["RV_LLEGADA"].ToString()))
                                {
                                    campo = reader2.GetName(16);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_LLEGADA"].ToString(), rm.rva_llegada, mh);
                                }
                                if (!rm.rva_salida.Equals(reader2["RV_SALIDA"].ToString()))
                                {
                                    campo = reader2.GetName(17);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_SALIDA"].ToString(), rm.rva_salida, mh);
                                }
                                if (!rm.rva_noches.Equals(Convert.ToInt32(reader2["RV_NOCHES"].ToString())))
                                {
                                    campo = reader2.GetName(18);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_NOCHES"].ToString(), rm.rva_noches.ToString(), mh);
                                }
                                if (!rm.rva_tipo_hab.Equals(reader2["RV_TIPO_HAB"].ToString()))
                                {
                                    campo = reader2.GetName(21);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_TIPO_HAB"].ToString(), rm.rva_tipo_hab, mh);
                                }
                                if (!rm.rva_tarifa.Equals(reader2["RV_TARIFA"].ToString()))
                                {
                                    campo = reader2.GetName(22);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_TARIFA"].ToString(), rm.rva_tarifa, mh);
                                }
                                if (!rm.rva_adulto.Equals(Convert.ToInt32(reader2["RV_ADULTO"].ToString())))
                                {
                                    campo = reader2.GetName(23);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_ADULTO"].ToString(), rm.rva_adulto.ToString(), mh);
                                }
                                if (!rm.rva_menor.Equals(Convert.ToInt32(reader2["RV_MENOR"].ToString())))
                                {
                                    campo = reader2.GetName(24);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_MENOR"].ToString(), rm.rva_menor.ToString(), mh);
                                }
                                if (!rm.rva_fase.Equals(reader2["RV_FASE"].ToString()))
                                {
                                    campo = reader2.GetName(26);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_FASE"].ToString(), rm.rva_fase, mh);
                                }
                                if (!rm.rva_reserva_bookingchanel.Equals(reader2["RV_VOUCHER"].ToString()))
                                {
                                    campo = reader2.GetName(33);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_VOUCHER"].ToString(), rm.rva_reserva_bookingchanel, mh);
                                }
                                if (!rm.rva_importe.Equals(reader2["RV_IMPORTE"].ToString()))
                                {
                                    campo = reader2.GetName(34);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_IMPORTE"].ToString(), rm.rva_importe, mh);
                                }
                                if (!rm.rva_meal_plancode.Equals(reader2["RV_PLANES"].ToString()))
                                {
                                    campo = reader2.GetName(61);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_PLANES"].ToString(), rm.rva_meal_plancode, mh);
                                }
                                if (!rm.rva_hab_renta.Equals(reader2["RV_TIPO_HAB_RENTA"].ToString()))
                                {
                                    campo = reader2.GetName(86);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_TIPO_HAB_RENTA"].ToString(), rm.rva_hab_renta, mh);
                                }
                                if (!rm.rva_hotel_renta.Equals(reader2["RV_HOTEL_RENTA"].ToString()))
                                {
                                    campo = reader2.GetName(87);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_HOTEL_RENTA"].ToString(), rm.rva_hotel_renta, mh);
                                }
                                if (!rm.rva_email.Equals(reader2["RV_EMAIL"].ToString()))
                                {
                                    campo = reader2.GetName(88);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_EMAIL"].ToString(), rm.rva_email, mh);
                                }
                                if (!rm.rva_moneda.Equals(reader2["RV_MONEDA"].ToString()))
                                {
                                    campo = reader2.GetName(89);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_MONEDA"].ToString(), rm.rva_moneda, mh);
                                }
                                if (!rm.rva_tc.Equals(Convert.ToDouble(reader2["RV_TC"].ToString()))){
                                    campo = reader2.GetName(93);
                                    ok = insertaModifi(rm.rva_oasis_rva, campo, reader2["RV_TC"].ToString(), rm.rva_tc.ToString(), mh);
                                }
                            }
                        }
                        else
                        {
                            rm.rva_err_recordid = rm.rva_reserva_bookingchanel;
                            rm.rva_err_value = "NO SE PUDO ENCONTRAR LA RESERVA EN EL PMS";
                            return false;
                        }

                    } catch (Exception ex)
                    {

                    }
                    // hace un update de la reservacion completa
                    string sql2 = "update freserva set rv_mayorista = :mayorista, rv_agencia = :agencia, rv_pais = :pais, " +
                        "rv_tipo_huesped = :tipo_huesped, rv_origen = :origen, rv_procede = :procede, rv_notas = :notas, rv_llegada = :llegada, " +
                        "rv_salida = :salida, rv_noches = :noches, rv_llegada_h = :hora_llegada, rv_salida_h = :hora_salida, rv_tipo_hab = :hab_renta, " +
                        "rv_tarifa = :tarifa, rv_adulto = :adulto, rv_menor = :menor, rv_fase = :fase, rv_voucher :voucher, rv_importe = :importe, " +
                        "rv_deposito = :deposito, rv_cap_f = :fecha_actual, rv_cap_h = :hora_actual, rv_cap_u = :usuario_captura, rv_planes = :planes, " +
                    "rv_tipo_hab_renta = :hab_renta, rv_hotel_renta : :hotel_renta, rv_email = :email, rv_moneda = :moneda, rv_tipo_venta = :tipo_venta, " +
                    "rv_dir1 = :address1, rv_dir2 = :address2 where rv_reserva = :num_reserva";
                    cmd.CommandText = sql2;
                    cmd.Parameters.Add("mayorista", OracleDbType.Varchar2).Value = rm.rva_mayorista;
                    cmd.Parameters.Add("agencia", OracleDbType.Varchar2).Value = rm.rva_agen;
                    cmd.Parameters.Add("pais", OracleDbType.Varchar2).Value = rm.rva_pais;
                    cmd.Parameters.Add("tipo_huesped", OracleDbType.Varchar2).Value = rm.rva_tipo_huesped;
                    cmd.Parameters.Add("origen", OracleDbType.Varchar2).Value = "CH";
                    cmd.Parameters.Add("procede", OracleDbType.Varchar2).Value = rm.rva_mayorista;
                    cmd.Parameters.Add("notas", OracleDbType.Varchar2).Value = rm.rva_notas;
                    cmd.Parameters.Add("llegada", OracleDbType.Date).Value = fllegada;
                    cmd.Parameters.Add("salida", OracleDbType.Date).Value = fsalida;
                    cmd.Parameters.Add("hora_llegada", OracleDbType.Varchar2).Value = "15:00";
                    cmd.Parameters.Add("hora_salida", OracleDbType.Varchar2).Value = "12:00";
                    cmd.Parameters.Add("hab_renta", OracleDbType.Varchar2).Value = rm.rva_tipo_hab;
                    cmd.Parameters.Add("tarifa", OracleDbType.Varchar2).Value = rm.rva_tarifa;
                    cmd.Parameters.Add("adulto", OracleDbType.Int32).Value = rm.rva_adulto;
                    cmd.Parameters.Add("menor", OracleDbType.Int32).Value = rm.rva_menor;
                    cmd.Parameters.Add("fase", OracleDbType.Varchar2).Value = rm.rva_fase;
                    cmd.Parameters.Add("voucher", OracleDbType.Varchar2).Value = rm.rva_reserva_bookingchanel;
                    cmd.Parameters.Add("importe", OracleDbType.Double).Value = Convert.ToDouble(rm.rva_importe).ToString(CultureInfo.InvariantCulture);
                    cmd.Parameters.Add("deposito", OracleDbType.Int32).Value = 3;
                    cmd.Parameters.Add("fecha_actual", OracleDbType.Date).Value = fecha_actual;
                    cmd.Parameters.Add("hora_actual", OracleDbType.Varchar2).Value = hora_actual;
                    cmd.Parameters.Add("usuario_captura", OracleDbType.Varchar2).Value = "OMBEES";
                    cmd.Parameters.Add("status", OracleDbType.Varchar2).Value = "R";
                    cmd.Parameters.Add("planes", OracleDbType.Varchar2).Value = rm.rva_meal_plancode;
                    cmd.Parameters.Add("hab_renta", OracleDbType.Varchar2).Value = rm.rva_hab_renta;
                    cmd.Parameters.Add("hotel_renta", OracleDbType.Varchar2).Value = rm.rva_hotel_renta;
                    cmd.Parameters.Add("email", OracleDbType.Varchar2).Value = rm.rva_email;
                    cmd.Parameters.Add("moneda", OracleDbType.Varchar2).Value = rm.rva_moneda;
                    cmd.Parameters.Add("tipo_venta", OracleDbType.Varchar2).Value = "ONLINE";
                    cmd.Parameters.Add("address1", OracleDbType.Varchar2).Value = dir1;
                    cmd.Parameters.Add("address2", OracleDbType.Varchar2).Value = dir2;
                    cmd.Parameters.Add("num_reserva", OracleDbType.Varchar2).Value = rm.rva_oasis_rva;
                    int rowsInsert2 = cmd.ExecuteNonQuery();
                    break;
            }
            con.Close();
            cmd.Dispose();
            con.Dispose();
            return ok;
        }

        private bool insertaModifi(string reserva, string campo, string antes, string despues, MapeoHoteles mh)
        {
            DateTime Hoy = DateTime.Today;
            string fecha_actual = Hoy.ToString();
            fecha_actual = fecha_actual.Substring(0, 10);
            string hora_actual = DateTime.Now.ToString("hh:mm");

            string StringConexion = "User Id=" + mh.Hotel_un + ";Password=" + mh.Hotel_pw + "; Data Source=" + mh.Hotel_ip + ":1521/" + mh.Hotel_cn + "; Pooling=false";
            OracleConnection con = new OracleConnection();
            con.ConnectionString = StringConexion;
            OracleCommand cmd = new OracleCommand();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "insert into frmodifi (mo_reserva,mo_mov_f,mo_mov_h,mo_mov_u,mo_mov_t,mo_campo,mo_antes,mo_despues,mo_motivo) " +
                "values ('"+reserva+"', '"+ fecha_actual + "', '"+ hora_actual + "', 'OMBEES', 'SYS', '"+ campo + "', '"+antes+"', '"+despues+"', null)";

            int rowsInsert = cmd.ExecuteNonQuery();

            cmd.Dispose();
            con.Dispose();

            return rowsInsert > 0;
        }
    }
}