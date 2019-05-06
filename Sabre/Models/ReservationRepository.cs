using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using System.Net.Http;
using System.Text;
using System.Globalization;
using System.Net.Http.Formatting;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;


namespace Sabre.Models
{
    public class ReservationRepository
    {
        private string fecha_actual;
        private string hora_actual;

        public string ProcesaReservacion(OTA_HotelResNotifRQ reservation)
        {
            ReservationModel rv = new ReservationModel();
            int rowsInsert = 0;
            List<HotelReservationIDrs> reservations = new List<HotelReservationIDrs>();
            bool ok = false;

            try
            {
                rv.rva_echotoken = reservation.EchoToken;

                foreach (HotelReservation g in reservation.HotelReservations.HotelReservation)
                {
                    // ResGlobalInfo
                    if (g.ResGlobalInfo != null)
                    {
                        if (g.ResGlobalInfo.HotelReservationIDs.HotelReservationID != null)
                        {
                            foreach (HotelReservationID rid in g.ResGlobalInfo.HotelReservationIDs.HotelReservationID)
                            {
                                if (rid.ResID_Source == "Omnibees")
                                {
                                    rv.rva_reserva_omnibees = rid.ResID_Value;
                                }
                                else if (rid.ResID_Source == "BookingChannel")
                                {
                                    rv.rva_reserva_bookingchanel = rid.ResID_Value;
                                }
                                else if (rid.ResID_Source == "PMS")
                                {
                                    rv.rva_oasis_rva = rid.ResID_Value;
                                }
                                else
                                {
                                    rv.rva_reserva_bookingchanel = rid.ResID_Value;
                                }
                            }
                        }
                    } // ResGlobalInfo

                    // obtengo el canal o agencia
                    foreach (Source s in g.POS.Source)
                    {
                        rv.rva_chanelname_obees = s.BookingChannel.CompanyName.Value;
                    }

                    // obtengo el hotel de renta en Oasis
                    rv.rva_cod_hotel_ob = g.ResGlobalInfo.BasicPropertyInfo.HotelCode;
                    MapeoHoteles mapeoHotelRenta = new MapeoHoteles();
                    mapeoHotelRenta = mapeoHotelRenta.ObtenerParamsBDHotel(rv.rva_cod_hotel_ob);
                    rv.rva_hotel_renta = mapeoHotelRenta.Hotel_os;
                    rv.rva_fase = mapeoHotelRenta.Hotel_fase;

                    rv.rva_action = g.ResStatus;

                    switch (rv.rva_action)
                    {
                        case "Booked":
                            {
                                rv.rva_oaction = "B";
                                // primero reviso si ya existe la reservacion en cielo
                                // si no, hago todo el proceso de insercion
                                ok = obtenerReservaCielo(mapeoHotelRenta, ref rv);
                                if (!ok)
                                {
                                    //obtengo el numero de reservacion en Oasis
                                    rv.rva_oasis_rva = ObtenerIdReservacion(mapeoHotelRenta);

                                    // reviso si regresa el numero de reserva de Oasis
                                    if (string.IsNullOrEmpty(rv.rva_oasis_rva))
                                    {
                                        rv.rva_oasis_errdesc = "No se pudo obtener el número de reserva de Oasis";
                                        return getErrorMessage(ref rv);
                                    }
                                }
                                else
                                {
                                    // hago un update a la tabla de logs para saber
                                    // que la reserva ya fue procesada
                                    int result = insertaNotasEnOB("La reserva ya se encontraba en el PMS con el numero " +
                                        rv.rva_oasis_rva + " y el Usuario " + rv.rva_cap_u, rv.rva_reserva_bookingchanel, 
                                        rv.rva_oasis_rva);
                                    rv.rva_oasis_errdesc = "La Reservación: " + rv.rva_oasis_rva + " ya se encuentra en el PMS.";
                                    return getSuccessMessage(rv);
                                }

                                foreach (RoomStay rs in g.RoomStays.RoomStay)
                                {
                                    // valido si la fecha fue correcta
                                    DateTime StartDate;
                                    if (DateTime.TryParse(rs.TimeSpan.Start, out StartDate))
                                    {
                                        rv.rva_llegada = rs.TimeSpan.Start;
                                    }
                                    else
                                    {
                                        rv.rva_have_error = true;
                                        rv.rva_error_code = "StartDateIsInvalid";
                                        rv.rva_error_type = "Unspecified";
                                        rv.rva_oasis_errdesc = "The arrival date has the wrong format";
                                        //rv.rva_oaction = "E";
                                    }

                                    // tambien la de salida
                                    DateTime EndDate;
                                    if (DateTime.TryParse(rs.TimeSpan.End, out EndDate))
                                    {
                                        rv.rva_salida = rs.TimeSpan.End;
                                    }
                                    else
                                    {
                                        rv.rva_have_error = true;
                                        rv.rva_error_code = "EndDateIsInvalid";
                                        rv.rva_error_type = "Unspecified";
                                        rv.rva_oasis_errdesc = "The date of departure has the wrong format";
                                        //rv.rva_oaction = "E";
                                    }

                                    foreach (RoomRate rr in rs.RoomRates.RoomRate)
                                    {
                                        rv.rva_hab_renta = rr.RoomTypeCode;
                                        rv.rva_tarifa = rr.RoomTypeCode; // es el mismo que el codigo de habitacion
                                        rv.rva_agen = rr.RatePlanCode;
                                    }

                                    rv.rva_adulto = 0;
                                    rv.rva_junior = 0;
                                    rv.rva_menor = 0;
                                    rv.rva_bebe = 0;

                                    foreach (GuestCount gc in rs.GuestCounts.GuestCount)
                                    {
                                        // valido los adultos - juniors - menores - bebes
                                        if (gc.AgeQualifyingCode == 10)
                                        {
                                            // adulto
                                            rv.rva_adulto += gc.Count;
                                        }
                                        else if (gc.AgeQualifyingCode == 8)
                                        {
                                            // junior
                                            if (gc?.Age >= 0 && gc?.Age <= 12)
                                            {
                                                rv.rva_menor += gc.Count;
                                            }
                                            else if (gc?.Age >= 13 && gc?.Age <= 17)
                                            {
                                                rv.rva_adulto += gc.Count;
                                            }
                                            else
                                            {
                                                rv.rva_menor += gc.Count;
                                            }
                                        } // end huepedes
                                    } // GuestCount
                                } // roomStay

                                // ResGuests
                                if (g.ResGuests != null)
                                {
                                    foreach (ProfileInfo pi in g.ResGuests.ResGuest[0].Profiles.ProfileInfo)
                                    {
                                        rv.rva_nameprefix = pi.Profile.Customer.PersonName?.NamePrefix;
                                        rv.rva_nombre = pi.Profile.Customer.PersonName?.GivenName;
                                        rv.rva_apell = pi.Profile.Customer.PersonName?.Surname;
                                        rv.rva_email = pi.Profile.Customer?.Email;
                                        // Address
                                        if (pi.Profile.Customer.Address != null)
                                        {
                                            rv.rva_pais = pi.Profile.Customer.Address.CountryName?.Code;

                                            if (string.IsNullOrEmpty(rv.rva_pais))
                                            {
                                                rv.rva_pais = "MX";
                                            }

                                            // Address
                                            for (int i = 0; i < pi.Profile.Customer.Address.AddressLine.Count; i++)
                                            {
                                                if (i == 0)
                                                {
                                                    rv.rva_address1 = pi.Profile.Customer.Address.AddressLine[0];
                                                }
                                                else
                                                {
                                                    rv.rva_address2 = pi.Profile.Customer.Address.AddressLine[1];
                                                }
                                            }
                                        }
                                    }
                                }

                                // ResGlobalInfo
                                if (g.ResGlobalInfo != null)
                                {
                                    rv.rva_chaincode = g.ResGlobalInfo.BasicPropertyInfo.ChainCode;
                                    rv.rva_moneda = g.ResGlobalInfo.Total.CurrencyCode;
                                    if (rv.rva_moneda == "MXN")
                                    {
                                        rv.rva_moneda = "MEX";
                                        rv.rva_tc = 1.0;
                                    }
                                    // obtengo las notas
                                    rv.rva_notas = g.ResGlobalInfo.Comments?.Comment.Text;
                                    //rv.rva_notas = Regex.Replace(g.ResGlobalInfo.Comments.Comment.Text, @"[^0-9A-Za-z]", "", RegexOptions.None);
                                    rv.rva_tot_amount_bef_tax = g.ResGlobalInfo.Total.AmountBeforeTax;
                                    rv.rva_tot_amount_aft_tax = g.ResGlobalInfo.Total.AmountAfterTax;
                                } // ResGlobalInfo

                                // valido el plan 
                                switch (rv.rva_mealplan)
                                {
                                    case "1":
                                        rv.rva_meal_plancode = "AI";
                                        break;
                                    case "14":
                                        rv.rva_meal_plancode = "EP";
                                        break;
                                    case "19":
                                        rv.rva_meal_plancode = "DI";
                                        break;
                                    default:
                                        rv.rva_meal_plancode = "AI";
                                        break;
                                }

                                // obtengo el mayorista por medio del codigo de agencia
                                rv.rva_mayorista = ObtenerMayorista(rv, mapeoHotelRenta);
                                rv.rva_tipo_huesped = "WB";

                                // diferencia de fechas para determinar las noches
                                System.TimeSpan ts = Convert.ToDateTime(rv.rva_salida) - Convert.ToDateTime(rv.rva_llegada);
                                rv.rva_noches = ts.Days;

                                // inserto la reserva en OB
                                bool okOB = InsertaReservaOB(rv, mapeoHotelRenta);

                                // inserta la nota en un metodo y tablas aparte por el tamaño
                                bool oknota = insertaNota(rv, mapeoHotelRenta);

                                ReservationModel rm = new ReservationModel();
                                ok = rm.interfazarReservasHotel(mapeoHotelRenta, ref rv);
                                // inserto el nombre del huesped
                                if (ok)
                                {
                                    ok = insertaNombreHuesped(rv, mapeoHotelRenta);
                                }
                                else
                                {
                                    return getErrorMessage(ref rv);
                                }

                                //inserta en freserpl
                                string sqlpl = "insert into freserpl (vp_reserva,vp_secuencia,vp_plan) values ('" + rv.rva_oasis_rva + "', " +
                                "1, '" + rv.rva_meal_plancode + "')";
                                string stringConexionpl = "User Id=" + mapeoHotelRenta.Hotel_un + ";Password=" + mapeoHotelRenta.Hotel_pw + ";Data Source=" + mapeoHotelRenta.Hotel_ip + ":1521/" + mapeoHotelRenta.Hotel_cn + ";Pooling=false";
                                OracleConnection conpl = new OracleConnection();
                                conpl.ConnectionString = stringConexionpl;
                                OracleCommand cmdpl = new OracleCommand();
                                conpl.Open();
                                cmdpl = conpl.CreateCommand();
                                cmdpl.CommandType = System.Data.CommandType.Text;
                                cmdpl.CommandText = sqlpl;
                                rowsInsert = cmdpl.ExecuteNonQuery();
                                cmdpl.Dispose();
                                conpl.Close();

                                string stringConexion = "User Id=" + mapeoHotelRenta.Hotel_un + ";Password=" + mapeoHotelRenta.Hotel_pw + ";Data Source=" + mapeoHotelRenta.Hotel_ip + ":1521/" + mapeoHotelRenta.Hotel_cn + ";Pooling=false";

                                if (g.RoomStays.RoomStay[0].RoomRates.RoomRate[0].Rates != null)
                                {
                                    // hago el calculo de la auditoria
                                    foreach (Rate rt in g.RoomStays.RoomStay[0].RoomRates.RoomRate[0].Rates.Rate)
                                    {
                                        DateTime dEfectiva = Convert.ToDateTime(rt.EffectiveDate);
                                        string fecha_fectiva = dEfectiva.ToString("dd/MM/yyyy");
                                        DateTime Hoy = DateTime.Today;
                                        string fecha_actual = Hoy.ToString();
                                        fecha_actual = fecha_actual.Substring(0, 10);
                                        string hora_actual = DateTime.Now.ToString("hh:mm");

                                        string sqlPR = "insert into FRESERVAM (rm_reserva,rm_fecha,rm_promo,rm_importe,rm_moneda,rm_cap_u,rm_cap_f, " +
                                            "rm_cap_h,rm_activo) values ('" + rv.rva_oasis_rva + "', '" + fecha_fectiva + "', null, " +
                                            "" + Convert.ToDouble(rt.Total.AmountAfterTax).ToString(CultureInfo.InvariantCulture) + ", " +
                                            "'" + rv.rva_moneda + "', 'OMBEES', '" + fecha_actual + "', '" + hora_actual + "', 'A')";

                                        stringConexion = "User Id=" + mapeoHotelRenta.Hotel_un + ";Password=" + mapeoHotelRenta.Hotel_pw + ";Data Source=" + mapeoHotelRenta.Hotel_ip + ":1521/" + mapeoHotelRenta.Hotel_cn + ";Pooling=false";
                                        OracleConnection con3 = new OracleConnection();
                                        con3.ConnectionString = stringConexion;
                                        OracleCommand cmd3 = new OracleCommand();
                                        con3.Open();
                                        cmd3 = con3.CreateCommand();
                                        cmd3.CommandType = System.Data.CommandType.Text;
                                        cmd3.CommandText = sqlPR;
                                        rowsInsert = cmd3.ExecuteNonQuery();
                                        cmd3.Dispose();
                                        con3.Close();
                                    }
                                } // Rates

                                // consumo la funcion: CALCULA_RESERVACION para que haga las operaciones contables
                                string stringConexion2 = "User Id=" + mapeoHotelRenta.Hotel_un + ";Password=" + mapeoHotelRenta.Hotel_pw + ";Data Source=" + mapeoHotelRenta.Hotel_ip + ":1521/" + mapeoHotelRenta.Hotel_cn + ";Pooling=false";
                                OracleConnection con2 = new OracleConnection();
                                con2.ConnectionString = stringConexion2;
                                OracleCommand cmd2 = new OracleCommand();
                                try
                                {
                                    cmd2.Connection = con2;

                                    cmd2.CommandText = "CALCULA_RESERVACION";
                                    cmd2.CommandType = CommandType.StoredProcedure;

                                    cmd2.Parameters.Add("renta", OracleDbType.Int32);
                                    cmd2.Parameters["renta"].Direction = ParameterDirection.ReturnValue;

                                    cmd2.Parameters.Add("vsesion", OracleDbType.Varchar2);
                                    cmd2.Parameters["vsesion"].Value = null;

                                    cmd2.Parameters.Add("vusuario", OracleDbType.Varchar2);
                                    cmd2.Parameters["vusuario"].Value = null;

                                    cmd2.Parameters.Add("vreserva", OracleDbType.Varchar2);
                                    cmd2.Parameters["vreserva"].Value = rv.rva_oasis_rva;

                                    cmd2.Parameters.Add("vmayorista", OracleDbType.Varchar2);
                                    cmd2.Parameters["vmayorista"].Value = null;

                                    cmd2.Parameters.Add("vagencia", OracleDbType.Varchar2);
                                    cmd2.Parameters["vagencia"].Value = null;

                                    cmd2.Parameters.Add("vgrupo", OracleDbType.Varchar2);
                                    cmd2.Parameters["vgrupo"].Value = null;

                                    cmd2.Parameters.Add("vllegada", OracleDbType.Date);
                                    cmd2.Parameters["vllegada"].Value = null;

                                    cmd2.Parameters.Add("vnoches", OracleDbType.Int32);
                                    cmd2.Parameters["vnoches"].Value = null;

                                    cmd2.Parameters.Add("vsalida", OracleDbType.Date);
                                    cmd2.Parameters["vsalida"].Value = null;

                                    cmd2.Parameters.Add("vadulto", OracleDbType.Int32);
                                    cmd2.Parameters["vadulto"].Value = null;

                                    cmd2.Parameters.Add("vjunior", OracleDbType.Int32);
                                    cmd2.Parameters["vjunior"].Value = null;

                                    cmd2.Parameters.Add("vmenor", OracleDbType.Int32);
                                    cmd2.Parameters["vmenor"].Value = null;

                                    cmd2.Parameters.Add("vbebe", OracleDbType.Int32);
                                    cmd2.Parameters["vbebe"].Value = null;

                                    cmd2.Parameters.Add("vtarifa", OracleDbType.Varchar2);
                                    cmd2.Parameters["vtarifa"].Value = null;

                                    cmd2.Parameters.Add("vhotel_renta", OracleDbType.Varchar2);
                                    cmd2.Parameters["vhotel_renta"].Value = null;

                                    cmd2.Parameters.Add("vpromocion", OracleDbType.Varchar2);
                                    cmd2.Parameters["vpromocion"].Value = null;

                                    cmd2.Parameters.Add("vcaptura", OracleDbType.Date);
                                    cmd2.Parameters["vcaptura"].Value = null;

                                    cmd2.Parameters.Add("vrecalculo", OracleDbType.Varchar2);
                                    cmd2.Parameters["vrecalculo"].Value = null;
                                    cmd2.Connection.Open();
                                    cmd2.ExecuteNonQuery();
                                    double renta = Convert.ToDouble(cmd2.Parameters["renta"].Value.ToString());
                                }
                                catch (Exception e)
                                {

                                }
                                finally
                                {
                                    cmd2.Connection.Close();
                                }
                            }

                            break;
                        case "Modified":
                            {
                                rv.rva_oaction = "M";
                                foreach (RoomStay rs in g.RoomStays.RoomStay)
                                {
                                    // valido si la fecha fue correcta
                                    DateTime StartDate;
                                    if (DateTime.TryParse(rs.TimeSpan.Start, out StartDate))
                                    {
                                        rv.rva_llegada = rs.TimeSpan.Start;
                                    }
                                    else
                                    {
                                        rv.rva_have_error = true;
                                        rv.rva_error_code = "StartDateIsInvalid";
                                        rv.rva_error_type = "Unspecified";
                                        rv.rva_oasis_errdesc = "The arrival date has the wrong format";
                                        //rv.rva_oaction = "E";
                                    }

                                    // tambien la de salida
                                    DateTime EndDate;
                                    if (DateTime.TryParse(rs.TimeSpan.End, out EndDate))
                                    {
                                        rv.rva_salida = rs.TimeSpan.End;
                                    }
                                    else
                                    {
                                        rv.rva_have_error = true;
                                        rv.rva_error_code = "EndDateIsInvalid";
                                        rv.rva_error_type = "Unspecified";
                                        rv.rva_oasis_errdesc = "The date of departure has the wrong format";
                                        //rv.rva_oaction = "E";
                                    }

                                    foreach (RoomRate rr in rs.RoomRates.RoomRate)
                                    {
                                        rv.rva_hab_renta = rr.RoomTypeCode;
                                        rv.rva_tarifa = rr.RoomTypeCode; // es el mismo que el codigo de habitacion
                                        rv.rva_agen = rr.RatePlanCode;
                                    }

                                    rv.rva_adulto = 0;
                                    rv.rva_junior = 0;
                                    rv.rva_menor = 0;
                                    rv.rva_bebe = 0;

                                    foreach (GuestCount gc in rs.GuestCounts.GuestCount)
                                    {
                                        // valido los adultos - juniors - menores - bebes
                                        if (gc.AgeQualifyingCode == 10)
                                        {
                                            // adulto
                                            rv.rva_adulto += gc.Count;
                                        }
                                        else if (gc.AgeQualifyingCode == 8)
                                        {
                                            // junior
                                            if (gc?.Age >= 0 && gc?.Age <= 12)
                                            {
                                                rv.rva_menor += gc.Count;
                                            }
                                            else if (gc?.Age >= 13 && gc?.Age <= 17)
                                            {
                                                rv.rva_adulto += gc.Count;
                                            }
                                            else
                                            {
                                                rv.rva_menor += gc.Count;
                                            }
                                        } // end huepedes
                                    } // GuestCount
                                } // roomStay

                                // ResGuests
                                if (g.ResGuests != null)
                                {
                                    foreach (ProfileInfo pi in g.ResGuests.ResGuest[0].Profiles.ProfileInfo)
                                    {
                                        rv.rva_nameprefix = pi.Profile.Customer.PersonName?.NamePrefix;
                                        rv.rva_nombre = pi.Profile.Customer.PersonName?.GivenName;
                                        rv.rva_apell = pi.Profile.Customer.PersonName?.Surname;
                                        rv.rva_email = pi.Profile.Customer?.Email;
                                        // Address
                                        if (pi.Profile.Customer.Address != null)
                                        {
                                            rv.rva_pais = pi.Profile.Customer.Address.CountryName?.Code;

                                            if (string.IsNullOrEmpty(rv.rva_pais))
                                            {
                                                rv.rva_pais = "MX";
                                            }

                                            // Address
                                            for (int i = 0; i < pi.Profile.Customer.Address.AddressLine.Count; i++)
                                            {
                                                if (i == 0)
                                                {
                                                    rv.rva_address1 = pi.Profile.Customer.Address.AddressLine[0];
                                                }
                                                else
                                                {
                                                    rv.rva_address2 = pi.Profile.Customer.Address.AddressLine[1];
                                                }
                                            }
                                        }

                                        // Document
                                        if (pi.Profile.Customer.Document != null)
                                        {
                                            foreach (Document doc in pi.Profile.Customer.Document)
                                            {
                                                rv.rva_doc_info = doc.DocID;
                                            }
                                        }
                                    } // foreach
                                } // ResGuests

                                // ResGlobalInfo
                                if (g.ResGlobalInfo != null)
                                {
                                    // reviso si regresa el numero de reserva de Oasis
                                    if (string.IsNullOrEmpty(rv.rva_oasis_rva))
                                    {
                                        rv.rva_have_error = true;
                                        rv.rva_error_code = "SystemError";
                                        rv.rva_error_type = "Unspecified";
                                        rv.rva_oasis_errdesc = "No se pudo obtener el número de reserva de Oasis";
                                        return getErrorMessage(ref rv);
                                    }

                                    rv.rva_chaincode = g.ResGlobalInfo.BasicPropertyInfo.ChainCode;
                                    rv.rva_moneda = g.ResGlobalInfo.Total.CurrencyCode;
                                    if (rv.rva_moneda == "MXN")
                                    {
                                        rv.rva_moneda = "MEX";
                                        rv.rva_tc = 1.0;
                                    }
                                    // obtengo las notas
                                    rv.rva_notas = g.ResGlobalInfo.Comments?.Comment.Text;
                                    //rv.rva_notas = Regex.Replace(g.ResGlobalInfo.Comments.Comment.Text, @"[^0-9A-Za-z]", "", RegexOptions.None);
                                    rv.rva_tot_amount_bef_tax = g.ResGlobalInfo.Total.AmountBeforeTax;
                                    rv.rva_tot_amount_aft_tax = g.ResGlobalInfo.Total.AmountAfterTax;
                                } // ResGlobalInfo

                                // si ya tengo una reserva
                                if (!string.IsNullOrEmpty(rv.rva_oasis_rva))
                                {
                                    // valido el plan 
                                    switch (rv.rva_mealplan)
                                    {
                                        case "1":
                                            rv.rva_meal_plancode = "AI";
                                            break;
                                        case "14":
                                            rv.rva_meal_plancode = "EP";
                                            break;
                                        case "19":
                                            rv.rva_meal_plancode = "DI";
                                            break;
                                        default:
                                            rv.rva_meal_plancode = "AI";
                                            break;
                                    }
                                    // obtengo el mayorista por medio del codigo de agencia
                                    rv.rva_mayorista = ObtenerMayorista(rv, mapeoHotelRenta);
                                    rv.rva_tipo_huesped = "WB";

                                    // diferencia de fechas para determinar las noches
                                    System.TimeSpan ts = Convert.ToDateTime(rv.rva_salida) - Convert.ToDateTime(rv.rva_llegada);
                                    rv.rva_noches = ts.Days;

                                    // inserta la nota en un metodo y tablas aparte por el tamaño
                                    bool oknota = insertaNota(rv, mapeoHotelRenta);

                                    ReservationModel rm = new ReservationModel();
                                    ok = rm.interfazarReservasHotel(mapeoHotelRenta, ref rv);
                                    // inserto el nombre del huesped
                                    if (ok)
                                    {
                                        ok = insertaNombreHuesped(rv, mapeoHotelRenta);
                                    }
                                    else
                                    {
                                        return getErrorMessage(ref rv);
                                    }

                                    // Rates
                                    string sqld = "delete from freservam where '" + rv.rva_oasis_rva + "'";
                                    string stringConexion = "User Id=" + mapeoHotelRenta.Hotel_un + ";Password=" + mapeoHotelRenta.Hotel_pw + ";Data Source=" + mapeoHotelRenta.Hotel_ip + ":1521/" + mapeoHotelRenta.Hotel_cn + ";Pooling=false";
                                    OracleConnection con = new OracleConnection();
                                    con.ConnectionString = stringConexion;
                                    OracleCommand cmd = new OracleCommand();
                                    con.Open();
                                    cmd = con.CreateCommand();
                                    cmd.CommandType = System.Data.CommandType.Text;
                                    cmd.CommandText = sqld;
                                    rowsInsert = cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                    con.Close();

                                    if (g.RoomStays.RoomStay[0].RoomRates.RoomRate[0].Rates != null)
                                    {
                                        // hago el calculo de la auditoria
                                        foreach (Rate rt in g.RoomStays.RoomStay[0].RoomRates.RoomRate[0].Rates.Rate)
                                        {
                                            DateTime dEfectiva = Convert.ToDateTime(rt.EffectiveDate);
                                            string fecha_fectiva = dEfectiva.ToString("dd/MM/yyyy");
                                            DateTime Hoy = DateTime.Today;
                                            string fecha_actual = Hoy.ToString();
                                            fecha_actual = fecha_actual.Substring(0, 10);
                                            string hora_actual = DateTime.Now.ToString("hh:mm");

                                            string sqlPR = "insert into FRESERVAM (rm_reserva,rm_fecha,rm_promo,rm_importe,rm_moneda,rm_cap_u,rm_cap_f, " +
                                                "rm_cap_h,rm_activo) values ('" + rv.rva_oasis_rva + "', '" + fecha_fectiva + "', null, " +
                                                "" + Convert.ToDouble(rt.Total.AmountAfterTax).ToString(CultureInfo.InvariantCulture) + ", " +
                                                "'" + rv.rva_moneda + "', 'OMBEES', '" + fecha_actual + "', '" + hora_actual + "', 'A')";

                                            stringConexion = "User Id=" + mapeoHotelRenta.Hotel_un + ";Password=" + mapeoHotelRenta.Hotel_pw + ";Data Source=" + mapeoHotelRenta.Hotel_ip + ":1521/" + mapeoHotelRenta.Hotel_cn + ";Pooling=false";
                                            OracleConnection con3 = new OracleConnection();
                                            con3.ConnectionString = stringConexion;
                                            OracleCommand cmd3 = new OracleCommand();
                                            con3.Open();
                                            cmd3 = con3.CreateCommand();
                                            cmd3.CommandType = System.Data.CommandType.Text;
                                            cmd3.CommandText = sqlPR;
                                            rowsInsert = cmd3.ExecuteNonQuery();
                                            cmd3.Dispose();
                                            con3.Close();
                                        }
                                    } // Rates

                                    // consumo la funcion: CALCULA_RESERVACION para que haga las operaciones contables
                                    string stringConexion2 = "User Id=" + mapeoHotelRenta.Hotel_un + ";Password=" + mapeoHotelRenta.Hotel_pw + ";Data Source=" + mapeoHotelRenta.Hotel_ip + ":1521/" + mapeoHotelRenta.Hotel_cn + ";Pooling=false";
                                    OracleConnection con2 = new OracleConnection();
                                    con2.ConnectionString = stringConexion2;
                                    OracleCommand cmd2 = new OracleCommand();
                                    try
                                    {
                                        cmd2.Connection = con2;

                                        cmd2.CommandText = "CALCULA_RESERVACION";
                                        cmd2.CommandType = CommandType.StoredProcedure;

                                        cmd2.Parameters.Add("renta", OracleDbType.Int32);
                                        cmd2.Parameters["renta"].Direction = ParameterDirection.ReturnValue;

                                        cmd2.Parameters.Add("vsesion", OracleDbType.Varchar2);
                                        cmd2.Parameters["vsesion"].Value = null;

                                        cmd2.Parameters.Add("vusuario", OracleDbType.Varchar2);
                                        cmd2.Parameters["vusuario"].Value = null;

                                        cmd2.Parameters.Add("vreserva", OracleDbType.Varchar2);
                                        cmd2.Parameters["vreserva"].Value = rv.rva_oasis_rva;

                                        cmd2.Parameters.Add("vmayorista", OracleDbType.Varchar2);
                                        cmd2.Parameters["vmayorista"].Value = null;

                                        cmd2.Parameters.Add("vagencia", OracleDbType.Varchar2);
                                        cmd2.Parameters["vagencia"].Value = null;

                                        cmd2.Parameters.Add("vgrupo", OracleDbType.Varchar2);
                                        cmd2.Parameters["vgrupo"].Value = null;

                                        cmd2.Parameters.Add("vllegada", OracleDbType.Date);
                                        cmd2.Parameters["vllegada"].Value = null;

                                        cmd2.Parameters.Add("vnoches", OracleDbType.Int32);
                                        cmd2.Parameters["vnoches"].Value = null;

                                        cmd2.Parameters.Add("vsalida", OracleDbType.Date);
                                        cmd2.Parameters["vsalida"].Value = null;

                                        cmd2.Parameters.Add("vadulto", OracleDbType.Int32);
                                        cmd2.Parameters["vadulto"].Value = null;

                                        cmd2.Parameters.Add("vjunior", OracleDbType.Int32);
                                        cmd2.Parameters["vjunior"].Value = null;

                                        cmd2.Parameters.Add("vmenor", OracleDbType.Int32);
                                        cmd2.Parameters["vmenor"].Value = null;

                                        cmd2.Parameters.Add("vbebe", OracleDbType.Int32);
                                        cmd2.Parameters["vbebe"].Value = null;

                                        cmd2.Parameters.Add("vtarifa", OracleDbType.Varchar2);
                                        cmd2.Parameters["vtarifa"].Value = null;

                                        cmd2.Parameters.Add("vhotel_renta", OracleDbType.Varchar2);
                                        cmd2.Parameters["vhotel_renta"].Value = null;

                                        cmd2.Parameters.Add("vpromocion", OracleDbType.Varchar2);
                                        cmd2.Parameters["vpromocion"].Value = null;

                                        cmd2.Parameters.Add("vcaptura", OracleDbType.Date);
                                        cmd2.Parameters["vcaptura"].Value = null;

                                        cmd2.Parameters.Add("vrecalculo", OracleDbType.Varchar2);
                                        cmd2.Parameters["vrecalculo"].Value = null;
                                        cmd2.Connection.Open();
                                        cmd2.ExecuteNonQuery();
                                        double renta = Convert.ToDouble(cmd2.Parameters["renta"].Value.ToString());
                                    }
                                    catch (Exception e)
                                    {
                                        throw e;
                                    }
                                    finally
                                    {
                                        cmd2.Connection.Close();
                                    }
                                } // end si ya tengo una reserva
                                else
                                {
                                    MapeoHoteles mh = new MapeoHoteles();
                                    mh = mh.ObtenerParamsBDHotel(rv.rva_cod_hotel_ob);
                                    // guardo el proceso que falló al tratar de obtener el id de la reserva
                                    bool ok5 = GuardarSuceso(mh.Hotel_siglas, rv.rva_reserva_omnibees, "NO SE PUDO OBTENER EL ID DE LA RESERVA PARA ESTE HOTEL");
                                    rv.rva_error = "OCURRIO UN ERROR EN EL GUARDADO DE LA RESERVA.";
                                    //return encapsulateSOAP(obtenerMensajeError(rv));
                                }
                                break;
                            }
                        case "Cancelled":
                            {
                                rv.rva_oaction = "C";
                                DateTime Hoy = DateTime.Today;
                                string fecha_actual = Hoy.ToString();
                                fecha_actual = fecha_actual.Substring(0, 10);
                                string hora_actual = DateTime.Now.ToString("hh:mm");
                                // verifico el numero de reservacion de la agencia
                                if (g.ResGlobalInfo.HotelReservationIDs.HotelReservationID != null)
                                {
                                    foreach (HotelReservationID rid in g.ResGlobalInfo.HotelReservationIDs.HotelReservationID)
                                    {
                                        if (rid.ResID_Source == "BookingChannel")
                                        {
                                            rv.rva_reserva_bookingchanel = rid.ResID_Value;
                                        }
                                        else if (rid.ResID_Source == "PMS")
                                        {
                                            rv.rva_oasis_rva = rid.ResID_Value;
                                        }
                                        else
                                        {
                                            rv.rva_reserva_bookingchanel = rid.ResID_Value;
                                        }
                                    }
                                }
                                // obtengo los parametros de conexion de la base de datos
                                rv.rva_cod_hotel_ob = g.ResGlobalInfo.BasicPropertyInfo.HotelCode;
                                MapeoHoteles mh = new MapeoHoteles();
                                mh = mh.ObtenerParamsBDHotel(rv.rva_cod_hotel_ob);

                                string sql3 = "update freserva set rv_status = 'C', rv_can_u = 'OMBEES', rv_can_c = 'OB', rv_can_f = '" + fecha_actual + "', " +
                                    "rv_can_h = '" + hora_actual + "' where rv_reserva = '" + rv.rva_oasis_rva + "' and rv_status = 'R'";

                                string stringConexion = "User Id=" + mh.Hotel_un + ";Password=" + mh.Hotel_pw + ";Data Source=" + mh.Hotel_ip + ":1521/" + mh.Hotel_cn + ";Pooling=false";
                                OracleConnection con = new OracleConnection();
                                con.ConnectionString = stringConexion;
                                OracleCommand cmd = new OracleCommand();
                                con.Open();
                                cmd = con.CreateCommand();
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = sql3;
                                rowsInsert = cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                con.Close();
                                break;
                            }
                    } // switch booked cancelled modified
                } // HotelReservation

                return getSuccessMessage(rv);

            } catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool InsertaReservaOB(ReservationModel rv, MapeoHoteles mph)
        {
            string hora_llegada = "15:00";
            string hora_salida = "12:00";
            int bebe = 0, junior = 0;
            int deposito = 3;
            string usuario_captura = "OMBEES";
            DateTime Hoy = DateTime.Today;
            fecha_actual = Hoy.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));
            hora_actual = DateTime.Now.ToString("hh:mm");
            rv.rva_origen = "CH";

            string StringConexion = ConfigurationManager.ConnectionStrings["PegasoConn"].ToString();
            OracleConnection con = new OracleConnection();
            con.ConnectionString = StringConexion;
            OracleCommand cmd = new OracleCommand();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = string.Format("insert into obreserva (ob_reserva, rv_mayorista, rv_agencia, rv_grupo, rv_pais, rv_tipo_huesped, rv_origen, rv_procede, " +
                "rv_notas, rv_llegada, rv_salida, rv_noches, rv_llegada_h, rv_salida_h, rv_tipo_hab, rv_tarifa, rv_adulto, rv_menor, rv_bebe, rv_fase, " +
                "rv_habi, rv_voucher, rv_importe, rv_deposito, rv_cap_f, rv_cap_h, rv_cap_u, rv_status, rv_planes, rv_tipo_hab_renta, " +
                "rv_hotel_renta, rv_email, rv_moneda, rv_prepago_f, rv_prepago_i, rv_promocion, rv_tc, rv_junior, rv_edificio, rv_tipo_venta, rv_action, rv_dir1, " +
                "rv_dir2, rv_reserva) values ('{0}'," +
                "'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',{11},'{12}','{13}','{14}','{15}',{16},{17},{18},'{19}','{20}','{21}',{22}," +
                "{23},'{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}',{34},'{35}',{36},{37},'{38}','{39}','{40}','{41}','{42}','{43}')",
                rv.rva_reserva_omnibees, rv.rva_mayorista, rv.rva_agen, rv.rva_grupo, rv.rva_pais, rv.rva_tipo_huesped, rv.rva_origen, rv.rva_mayorista,
                null,
                rv.rva_llegada, rv.rva_salida, rv.rva_noches, hora_llegada, hora_salida, rv.rva_hab_renta, rv.rva_tarifa, rv.rva_adulto, rv.rva_menor, bebe,
                rv.rva_fase, null, rv.rva_reserva_bookingchanel, Convert.ToDouble(rv.rva_tot_amount_aft_tax).ToString(CultureInfo.InvariantCulture), deposito,
                fecha_actual, hora_actual, usuario_captura, rv.rva_oaction, rv.rva_meal_plancode, rv.rva_hab_renta, rv.rva_hotel_renta, rv.rva_email,
                rv.rva_moneda, null, 0, null, rv.rva_tc, junior, null, null, rv.rva_oaction, rv.rva_address1, rv.rva_address2, rv.rva_oasis_rva);
            int rowsInsert = cmd.ExecuteNonQuery();

            cmd.Dispose();
            con.Dispose();

            return rowsInsert > 0;
        }

        private bool insertaNota(ReservationModel rv, MapeoHoteles mh)
        {
            string StringConexion = "User Id=" + mh.Hotel_un + ";Password=" + mh.Hotel_pw + "; Data Source=" + mh.Hotel_ip + ":1521/" + mh.Hotel_cn + "; Pooling=false";
            OracleConnection con = new OracleConnection();
            con.ConnectionString = StringConexion;
            OracleCommand cmd = new OracleCommand();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = "insert into freserobnot (ob_reserva,ob_nota,ob_hotel_renta) values " +
                "('" + rv.rva_oasis_rva + "', '" + rv.rva_notas.Replace("\'", "").Replace(";", "") + "', '" + rv.rva_hotel_renta + "')";
            int rowsInsert = cmd.ExecuteNonQuery();

            cmd.Dispose();
            con.Dispose();

            return rowsInsert > 0;
        }

        private bool GuardarSuceso(string Hotel, string Reservacion, string Suceso)
        {
            DateTime Hoy = DateTime.Today;
            fecha_actual = Hoy.ToString("dd/MM/yy", CultureInfo.CreateSpecificCulture("en-US"));
            hora_actual = DateTime.Now.ToString("hh:mm");

            string StringConexion = "User Id=omnibees;Password=service; Data Source=192.168.1.21:1521/pegaso; Pooling=false";
            OracleConnection con = new OracleConnection();
            con.ConnectionString = StringConexion;
            OracleCommand cmd = new OracleCommand();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = string.Format("insert into obmodifi (hotel, reservacion, suceso, fecha, hora) VALUES ('{0}','{1}','{2}','{3}','{4}')",
                Hotel, Reservacion, Suceso, fecha_actual, hora_actual);
            int rowsInsert = cmd.ExecuteNonQuery();

            cmd.Dispose();
            con.Dispose();

            return rowsInsert > 0;
        }

        private string obtenerMensajeSatisfaccion(ReservationModel rv)
        {
            string response = "";
            List<HotelReservationID> hrid = new List<HotelReservationID>();
            Random random = new Random();
            int randomNumber = random.Next(0, 10000);
            hrid.Add(new HotelReservationID() { ResID_Type = "10", ResID_Value = randomNumber.ToString(), ResID_Source = "PMS" });
            HotelReservationIDs hrids = new HotelReservationIDs();
            hrids.HotelReservationID = hrid;
            ResGlobalInfo rgi = new ResGlobalInfo();
            rgi.HotelReservationIDs = hrids;
            UniqueID ui = new UniqueID();
            ui.Type = "14";
            ui.ID = rv.rva_reserva_omnibees;
            List<HotelReservation> hr = new List<HotelReservation>();
            hr.Add(new HotelReservation() { UniqueID = ui, ResGlobalInfo = rgi });
            HotelReservations hrs = new HotelReservations();
            hrs.HotelReservation = hr;
            List<Warning> wr = new List<Warning>();
            Warnings wrs = new Warnings();
            wrs.Warning = wr;
            Success su = new Success();
            OTA_HotelResNotifRS objeto = new OTA_HotelResNotifRS();
            objeto.HotelReservations = hrs;
            objeto.EchoToken = rv.rva_echotoken;
            objeto.Version = "3.0";
            //objeto.Errors = rv.rva_error;
            objeto.TimeStamp = DateTime.Now.ToString("u");
            objeto.Warnings = wrs;
            objeto.Success = su;
            objeto.HotelReservations = hrs;
            Serializer serializer = new Serializer();
            response = serializer.Serialize(objeto);
            return response;
        }

        //public static string GetRandomAlphaNumeric()
        //{
        //    var random = new Random();
        //    var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        //    return new string(chars.Select(c => chars[random.Next(chars.Length)]).Take(8).ToArray());
        //}

        //private string obtenerMensajeError(ReservationModel rv)
        //{
        //    string response = "";
        //    List<HotelReservationID> hrid = new List<HotelReservationID>();
        //    Random random = new Random();
        //    int randomNumber = random.Next(0, 10000);
        //    hrid.Add(new HotelReservationID() { ResID_Type = "10", ResID_Value = randomNumber.ToString(), ResID_Source = "PMS" });
        //    HotelReservationIDs hrids = new HotelReservationIDs();
        //    hrids.HotelReservationID = hrid;
        //    ResGlobalInfo rgi = new ResGlobalInfo();
        //    rgi.HotelReservationIDs = hrids;
        //    UniqueID ui = new UniqueID();
        //    ui.Type = "14";
        //    ui.ID = rv.rva_reserva_omnibees;
        //    List<HotelReservation> hr = new List<HotelReservation>();
        //    hr.Add(new HotelReservation() { UniqueID = ui, ResGlobalInfo = rgi });
        //    HotelReservations hrs = new HotelReservations();
        //    hrs.HotelReservation = hr;
        //    List<Warning> wr = new List<Warning>();
        //    Warnings wrs = new Warnings();
        //    wrs.Warning = wr;
        //    Success su = new Success();
        //    OTA_HotelResNotifRS objeto = new OTA_HotelResNotifRS();
        //    objeto.HotelReservations = hrs;
        //    objeto.EchoToken = rv.rva_echotoken;
        //    objeto.Version = "3.0";
        //    //objeto.Errors = rv.rva_error;
        //    objeto.TimeStamp = DateTime.Now.ToString("u");
        //    objeto.Warnings = wrs;
        //    objeto.Success = su;
        //    objeto.HotelReservations = hrs;
        //    Serializer serializer = new Serializer();
        //    response = serializer.Serialize(objeto);
        //    return response;
        //}

        private string getSuccessMessage(ReservationModel rv)
        {
            string soap = "";
            soap += "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:con=\"http://connectors.omnibees.com/\" xmlns =\"http://www.opentravel.org/OTA/2003/05\" > ";
            soap += "<soapenv:Body>";
            soap += "<OTA_HotelResNotifRS EchoToken=\"" + rv.rva_echotoken + "\" TimeStamp=\"" + DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'") + "\" Version =\"3.00\" xmlns =\"http://www.opentravel.org/OTA/2003/05\" > ";
            soap += "<HotelReservations>";
            soap += "<HotelReservation>";
            soap += "<UniqueID Type=\"14\" ID =\"" + rv.rva_reserva_omnibees + "\" />";
            soap += "<ResGlobalInfo>";
            soap += "<HotelReservationIDs>";
            soap += "<HotelReservationID ResID_Type=\"10\" ResID_Value =\"" + rv.rva_oasis_rva + "\" ResID_Source =\"PMS\" />";
            soap += "</HotelReservationIDs>";
            soap += "</ResGlobalInfo>";
            soap += "</HotelReservation>";
            soap += "</HotelReservations>";
            soap += "<Warnings />";
            soap += "<Success />";
            soap += "</OTA_HotelResNotifRS>";
            soap += "</soapenv:Body>";
            soap += "</soapenv:Envelope>";
            string xmlReservA = rv.rva_reserva_bookingchanel.Replace("/", "-");
            File.WriteAllText(@"C:\ASP\WS\InterfaceOTA\documents\response\" + xmlReservA + ".xml", soap);

            return soap;
        }

        private string ObtenerMayorista(ReservationModel rv, MapeoHoteles mh)
        {
            string mayorista = "", StringConexion = "";
            StringConexion = "User Id=" + mh.Hotel_un + ";Password=" + mh.Hotel_pw + "; Data Source=" + mh.Hotel_ip + ":1521/" + mh.Hotel_cn + "; Pooling=false";
            OracleConnection con = new OracleConnection();
            OracleCommand cmd = new OracleCommand();

            try
            {

                con.ConnectionString = StringConexion;

                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "select ag_mayorista from fragen where ag_agencia = '" + rv.rva_agen + "' and ag_tipo = 'OTA' " +
                    "and ag_mayorista <> 'PQT'";
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    mayorista = reader["ag_mayorista"].ToString();
                }
            } catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Dispose();
            }
            return mayorista;
        }

        private string getErrorMessage(ref ReservationModel rv)
        {
            string soap = "";
            soap += "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:con=\"http://connectors.omnibees.com/\" xmlns =\"http://www.opentravel.org/OTA/2003/05\" > ";
            soap += "<soapenv:Body>";
            soap += "<OTA_HotelResNotifRS EchoToken=\"" + rv.rva_echotoken + "\" TimeStamp=\"" + DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'") + "\" Version =\"3.00\" xmlns =\"http://www.opentravel.org/OTA/2003/05\" > ";
            soap += "<Warnings />";
            soap += "<Errors>";
            soap += "<Error RecordID=\"" + rv.rva_oasis_errdesc + "</Error>";
            soap += "</Errors>";
            soap += "</OTA_HotelResNotifRS>";
            soap += "</soapenv:Body>";
            soap += "</soapenv:Envelope>";
            // guardo el xml del response
            rv.rva_reserva_bookingchanel = rv.rva_reserva_bookingchanel.Replace("/", "-");
            File.WriteAllText(@"C:\ASP\WS\InterfaceOTA\documents\response\" + rv.rva_reserva_bookingchanel + ".xml", soap);

            return soap;
        }

        private string ObtenerIdReservacion(MapeoHoteles mh)
        {
            string StringConexion = "", reservacion = "", sql = "", sql2 = "", sql3 = "",
                                    reservacionExiste = "", res = "";
            int count = 0, cuentaReserva = 0;
            StringConexion = "User Id=" + mh.Hotel_un + ";Password=" + mh.Hotel_pw + "; Data Source=" + mh.Hotel_ip + ":1521/" + mh.Hotel_cn + "; Pooling=false";

            using (OracleConnection con = new OracleConnection(StringConexion))
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                OracleCommand cmd2 = con.CreateCommand();
                OracleCommand cmd3 = con.CreateCommand();

                try
                {

                    // consulto el consecutivo de la base de datos
                    sql = "select pr_reserva from frparam";
                    cmd.CommandText = sql;
                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        reservacion = Convert.ToString(reader["PR_RESERVA"]);
                    }

                    while (count <= 0)
                    {
                        sql2 = "select rv_reserva from freserva where rv_reserva = '" + reservacion + "'";
                        cmd2.CommandText = sql2;
                        OracleDataReader reader2 = cmd2.ExecuteReader();
                        while (reader2.Read())
                        {
                            reservacionExiste = Convert.ToString(reader2["rv_reserva"]);
                        }

                        // si está vacia, quiere decir que no hay reservacion con ese numero y lo puedo usar
                        if (string.IsNullOrEmpty(reservacionExiste))
                        {
                            count++;
                            // actualizo el consecutivo para insertarlo en la bd
                            // sin afectar el que voy a utilizar
                            cuentaReserva = Convert.ToInt32(reservacion) + 1;
                            if (cuentaReserva.ToString().Length == 5)
                            {
                                res = "0" + Convert.ToString(cuentaReserva);
                            }
                            else
                            {
                                res = Convert.ToString(cuentaReserva);
                            }

                            // actualizo el consecutivo
                            sql3 = string.Format("update frparam set pr_reserva = '{0}'", res);
                            cmd3.CommandText = sql3;
                            int rowsInsert = cmd3.ExecuteNonQuery();
                        }
                        else
                        {
                            cuentaReserva = Convert.ToInt32(reservacion) + 1;
                            if (cuentaReserva.ToString().Length == 5)
                            {
                                reservacion = "0" + Convert.ToString(cuentaReserva);
                            }
                            else
                            {
                                reservacion = "0" + Convert.ToString(cuentaReserva);
                            }
                            // ponemos en cero la confirmacion de freserva
                            reservacionExiste ="";
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Dispose();
                    cmd2.Dispose();
                    cmd3.Dispose();
                    con.Dispose();
                }
            }
            return reservacion;
        }

        private bool insertaNombreHuesped(ReservationModel rv, MapeoHoteles mh)
        {
            int rowsInsert = 0;
            string nombre = "", apellido = "";

            if (rv.rva_nombre != null)
            {
                if (rv.rva_nombre.Length > 15)
                {
                    nombre = rv.rva_nombre.Substring(0, 15);
                }
                else
                {
                    nombre = rv.rva_nombre;
                }
                nombre = nombre.ToUpper();
            }
            else
            {
                nombre = "TBA";
            }

            if (rv.rva_apell != null)
            {
                if (rv.rva_apell.Length > 20)
                {
                    apellido = rv.rva_apell.Substring(0, 20);
                }
                else
                {
                    apellido = rv.rva_apell;
                }
                apellido = apellido.ToUpper();
            }
            else
            {
                apellido = "TBA";
            }

            string StringConexion = "User Id=" + mh.Hotel_un + ";Password=" + mh.Hotel_pw + "; Data Source=" + mh.Hotel_ip + ":1521/" + mh.Hotel_cn + "; Pooling=false";
            OracleConnection con = new OracleConnection();
            con.ConnectionString = StringConexion;
            OracleCommand cmd = new OracleCommand();
            try
            {
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "insert into FRESERNO (VN_RESERVA,VN_SECUENCIA,VN_APELLIDO,VN_NOMBRE) VALUES (" + "'"
                                    + rv.rva_oasis_rva + "', 1, '" + apellido + "'," + "'"
                                    + nombre + "')";
                rowsInsert = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                cmd.Dispose();
                con.Dispose();
            }
            return rowsInsert > 0;
        }

        private bool obtenerReservaCielo(MapeoHoteles mh, ref ReservationModel rv)
        {
            string StringConexion = "", bookingChannel = "";
            bool ok = false;

            StringConexion = "User Id=" + mh.Hotel_un + ";Password=" + mh.Hotel_pw + "; Data Source=" + mh.Hotel_ip + ":1521/" + mh.Hotel_cn + "; Pooling=false";
            OracleConnection con = new OracleConnection();
            OracleCommand cmd = new OracleCommand();

            try
            {
                con.ConnectionString = StringConexion;
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "select rv_reserva, rv_voucher, rv_cap_u from freserva where rv_voucher = '" + rv.rva_reserva_bookingchanel + "' and rv_status in('B', 'R') ";
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rv.rva_oasis_rva = reader["rv_reserva"].ToString();
                    bookingChannel = reader["rv_voucher"].ToString();
                    rv.rva_cap_u = reader["rv_cap_u"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Dispose();
            }
            // reviso si no regresa vacío o nulo
            if (!string.IsNullOrEmpty(bookingChannel))
            {
                ok = true;
            }
            return ok;
        }

        private int insertaNotasEnOB(string nota, string bookingChannel, string reservacion)
        {
            DateTime Hoy = DateTime.Today;
            hora_actual = DateTime.Now.ToString("HH:mm");
            fecha_actual = Hoy.ToString("dd/MM/yyyy");
            int rowCount = 0;

            string stringConexion = ConfigurationManager.ConnectionStrings["PegasoConn"].ToString();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = stringConexion;

            try
            {
                conn.Open();
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = string.Format("UPDATE OBRESERVA SET RV_RESERVA = '{0}', RV_NOTAS = '{1}', RV_MOD_F = '{2}', " +
                    "RV_MOD_H = '{3}' WHERE RV_VOUCHER = '{4}'", reservacion, nota, fecha_actual, hora_actual, bookingChannel);
                rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }

            return rowCount;
        }
    }
}

