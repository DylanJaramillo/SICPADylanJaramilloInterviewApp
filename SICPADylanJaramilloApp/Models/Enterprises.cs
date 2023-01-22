using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace SICPADylanJaramilloApp.Models
{
    public class Enterprises
    {
        //Variables
        public static NpgsqlConnection con = new NpgsqlConnection();






        #region SQLFunctions

        public static List<EnterprisesList> GetEnterprisesInfo(ref string result, string enterpriseInfo)
        {
            //Variables
            List<EnterprisesList> _enterprisesList = new List<EnterprisesList>();
            con = null;




            using (con = DBConnection.GetMySQLConnection())
            {
                try
                {
                    con.Open();

                    string postgreSQLCommand = "SELECT E.ID,E.CREATED_BY,E.CREATED_DATE,COALESCE(E.MODIFIED_BY,''),COALESCE(E.MODIFIED_DATE,'1800-01-01 02:02:02.260555'),(CASE WHEN E.STATUS = '1' THEN 'Active' ELSE CASE WHEN E.STATUS = '0' THEN 'Inactive' END END),E.ADDRESS,E.NAME,E.PHONE FROM enterprises E WHERE (LOWER(E.NAME) LIKE '%" + enterpriseInfo + "%' OR LOWER(E.ADDRESS) LIKE '%" + enterpriseInfo + "%') AND E.STATUS IN ('1','0') ORDER BY E.NAME ASC;";


                    using (NpgsqlCommand command = new NpgsqlCommand(postgreSQLCommand, con))
                    {





                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {


                            while (reader.HasRows && reader.Read())
                            {
                                EnterprisesList list = new EnterprisesList()
                                {
                                    id = reader.GetInt32(0),
                                    createdBy = reader.GetString(1),
                                    createdDate = reader.GetDateTime(2),
                                    modifiedBy = reader.GetString(3),
                                    modifiedDate = reader.GetDateTime(4),
                                    status = reader.GetString(5),
                                    address = reader.GetString(6),
                                    name = reader.GetString(7),
                                    phone = reader.GetString(8)
                                };
                                _enterprisesList.Add(list);
                            }
                        }

                    }

                    if (_enterprisesList.Count > 0)
                    {
                        result = "Success.";
                    }
                    else
                    {
                        result = "No data.";
                    }


                }
                catch (Exception ex)
                {
                    string message = new StringReader(ex.Message.ToString()).ReadLine();
                    result = message;
                }
                finally
                {
                    con.Close();
                }

                return _enterprisesList;
            }

        }


        public static void ConfirmExistingEnterprise(ref string result, string enterpriseName)
        {
            //Variables
            con = null;


            using (con = DBConnection.GetMySQLConnection())
            {
                try
                {
                    con.Open();

                    string postgreSQLCommand = "SELECT CASE WHEN COUNT(COALESCE(E.ID,0)) > 0 THEN 'EXIST' ELSE 'NOT EXIST' END FROM enterprises E WHERE LOWER(E.NAME) = '" + enterpriseName + "';";


                    using (NpgsqlCommand command = new NpgsqlCommand(postgreSQLCommand, con))
                    {


                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {


                            while (reader.HasRows && reader.Read())
                            {
                                result = reader.GetString(0);
                            }
                        }

                    }



                }
                catch (Exception ex)
                {
                    string message = new StringReader(ex.Message.ToString()).ReadLine();
                    result = message;
                }
                finally
                {
                    con.Close();
                }


            }
        }



        public static void ConfirmEditExistingEnterprise(ref string result, string enterpriseName, int id)
        {
            //Variables
            con = null;


            using (con = DBConnection.GetMySQLConnection())
            {
                try
                {
                    con.Open();

                    string postgreSQLCommand = "SELECT CASE WHEN COUNT(COALESCE(E.ID,0)) > 0 THEN 'EXIST' ELSE 'NOT EXIST' END FROM enterprises E WHERE E.ID !=" + id + " AND LOWER(E.NAME) = '" + enterpriseName + "';";


                    using (NpgsqlCommand command = new NpgsqlCommand(postgreSQLCommand, con))
                    {


                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {


                            while (reader.HasRows && reader.Read())
                            {
                                result = reader.GetString(0);
                            }
                        }

                    }



                }
                catch (Exception ex)
                {
                    string message = new StringReader(ex.Message.ToString()).ReadLine();
                    result = message;
                }
                finally
                {
                    con.Close();
                }


            }
        }



        public static void InsertUpdateDeleteEnterprise(ref string result, string action, string enterpriseName, string enterpriseAddress, string enterprisePhone,
            char enterpriseStatus, string createdBy, int id)
        {
            //Variables
            con = null;
            object answer;


            using (con = DBConnection.GetMySQLConnection())
            {
                try
                {
                    con.Open();

                    string postgreSQLCommand = "CALL sp_enterprises_insert_update_delete(:_action, :enterpriseName, :enterpriseAddress, :enterprisePhone, :_status, :createdBy, :_id, 1)";


                    using (NpgsqlCommand command = new NpgsqlCommand(postgreSQLCommand, con))
                    {

                        command.Parameters.AddWithValue("_action", DbType.String).Value = action;
                        command.Parameters.AddWithValue("enterpriseName", DbType.String).Value = enterpriseName;
                        command.Parameters.AddWithValue("enterpriseAddress", DbType.String).Value = enterpriseAddress;
                        command.Parameters.AddWithValue("enterprisePhone", DbType.String).Value = enterprisePhone;
                        command.Parameters.AddWithValue("_status", DbType.String).Value = enterpriseStatus;
                        command.Parameters.AddWithValue("createdBy", DbType.String).Value = createdBy;
                        command.Parameters.AddWithValue("_id", DbType.Int32).Value = id;



                        command.CommandType = CommandType.Text;


                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {


                            while (reader.HasRows && reader.Read())
                            {
                                answer = reader.GetValue(0);

                                result = answer.ToString();

                            }
                        }



                    }



                }
                catch (Exception ex)
                {
                    string message = new StringReader(ex.Message.ToString()).ReadLine();
                    result = message;
                }
                finally
                {
                    con.Close();
                }


            }
        }


        #endregion

    }
}