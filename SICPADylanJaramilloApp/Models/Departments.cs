using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace SICPADylanJaramilloApp.Models
{
    public class Departments
    {
        //Variables
        public static NpgsqlConnection con = new NpgsqlConnection();















        #region SQLFunctions

        public static List<DepartmentsList> GetDepartmentsInfo(ref string result, string departmentsInfo)
        {
            //Variables
            List<DepartmentsList> _departmentsList = new List<DepartmentsList>();
            con = null;




            using (con = DBConnection.GetMySQLConnection())
            {
                try
                {
                    con.Open();

                    string postgreSQLCommand = "SELECT D.ID,D.CREATED_BY,D.CREATED_DATE,COALESCE(D.MODIFIED_BY,''),COALESCE(D.MODIFIED_DATE,'1800-01-01 02:02:02.260555'),(CASE WHEN D.SATUS = '1' THEN 'Active' ELSE CASE WHEN D.SATUS = '0' THEN 'Inactive' END END),D.DESCRIPTION,D.NAME,D.PHONE, D.ID_ENTERPRISE, E.NAME FROM departments D JOIN enterprises E ON D.ID_ENTERPRISE = E.ID WHERE (LOWER(D.NAME) LIKE '%" + departmentsInfo + "%' OR LOWER(D.DESCRIPTION) LIKE '%" + departmentsInfo + "%'  OR LOWER(E.NAME) LIKE '%" + departmentsInfo + "%' ) AND D.SATUS IN ('1','0') AND E.STATUS = '1' ORDER BY D.NAME ASC;";


                    using (NpgsqlCommand command = new NpgsqlCommand(postgreSQLCommand, con))
                    {





                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {


                            while (reader.HasRows && reader.Read())
                            {
                                DepartmentsList list = new DepartmentsList()
                                {
                                    id = reader.GetInt32(0),
                                    createdBy = reader.GetString(1),
                                    createdDate = reader.GetDateTime(2),
                                    modifiedBy = reader.GetString(3),
                                    modifiedDate = reader.GetDateTime(4),
                                    status = reader.GetString(5),
                                    description = reader.GetString(6),
                                    name = reader.GetString(7),
                                    phone = reader.GetString(8),
                                    enterpriseID = reader.GetInt32(9),
                                    enterpriseName = reader.GetString(10)
                                };
                                _departmentsList.Add(list);
                            }
                        }

                    }

                    if (_departmentsList.Count > 0)
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

                return _departmentsList;
            }

        }


        public static void ConfirmExistingDepartments(ref string result, string departmentsName, string enterpriseName)
        {
            //Variables
            con = null;


            using (con = DBConnection.GetMySQLConnection())
            {
                try
                {
                    con.Open();

                    string postgreSQLCommand = "SELECT CASE WHEN COUNT(COALESCE(D.ID,0)) > 0 THEN 'EXIST' ELSE 'NOT EXIST' END FROM departments D JOIN enterprises E ON D.ID_ENTERPRISE = E.ID WHERE LOWER(D.NAME) = '" + departmentsName + "' AND LOWER(E.NAME) = '" + enterpriseName + "';";


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



        public static void ConfirmEditExistingDepartment(ref string result, string departmentName, int departmentId, string enterpriseName)
        {
            //Variables
            con = null;


            using (con = DBConnection.GetMySQLConnection())
            {
                try
                {
                    con.Open();

                    string postgreSQLCommand = "SELECT CASE WHEN COUNT(COALESCE(D.ID,0)) > 0 THEN 'EXIST' ELSE 'NOT EXIST' END FROM departments D JOIN enterprises E ON D.ID_ENTERPRISE = E.ID WHERE D.ID != " + departmentId + " AND LOWER(D.NAME) = '" + departmentName + "' AND LOWER(E.NAME) = '" + enterpriseName + "';";


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



        public static void InsertUpdateDeleteDepartments(ref string result, string action, string departmentName, string departmentAddress, string departmentPhone,
            char departmentStatus, string createdBy, int id, string enterpriseName)
        {
            //Variables
            con = null;
            object answer;


            using (con = DBConnection.GetMySQLConnection())
            {
                try
                {
                    con.Open();

                    string postgreSQLCommand = "CALL sp_departments_insert_update_delete(:_action, :departmentName, :departmentDescription, :departmentPhone, :_status, :createdBy, :_id, :_enterpriseName, 1)";


                    using (NpgsqlCommand command = new NpgsqlCommand(postgreSQLCommand, con))
                    {

                        command.Parameters.AddWithValue("_action", DbType.String).Value = action;
                        command.Parameters.AddWithValue("departmentName", DbType.String).Value = departmentName;
                        command.Parameters.AddWithValue("departmentDescription", DbType.String).Value = departmentAddress;
                        command.Parameters.AddWithValue("departmentPhone", DbType.String).Value = departmentPhone;
                        command.Parameters.AddWithValue("_status", DbType.String).Value = departmentStatus;
                        command.Parameters.AddWithValue("createdBy", DbType.String).Value = createdBy;
                        command.Parameters.AddWithValue("_id", DbType.Int32).Value = id;
                        command.Parameters.AddWithValue("_enterpriseName", DbType.String).Value = enterpriseName;



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



        public static List<String> GetEnterprisesName(ref string result)
        {
            //Variables
            List<string> _enterprisesNameList = new List<string>();
            con = null;


            using (con = DBConnection.GetMySQLConnection())
            {
                try
                {
                    con.Open();

                    string postgreSQLCommand = "SELECT E.ID,E.NAME FROM enterprises E WHERE E.STATUS = '1' ORDER BY E.NAME;";


                    using (NpgsqlCommand command = new NpgsqlCommand(postgreSQLCommand, con))
                    {





                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {


                            while (reader.HasRows && reader.Read())
                            {
                                string name = reader.GetString(1);
                                _enterprisesNameList.Add(name);
                            }
                        }

                    }

                    if (_enterprisesNameList.Count > 0)
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

                return _enterprisesNameList;
            }


        }


        #endregion

    }
}