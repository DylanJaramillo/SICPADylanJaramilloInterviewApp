using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SICPADylanJaramilloApp.Models
{
    public class DBConnection
    {
        //Info for string connection
        public static string serverHostName { get; set; }
        public static string port { get; set; }
        public static string userID { get; set; }
        public static string password { get; set; }
        public static string dataBaseName { get; set; }

        public static string completeConnection { get; set; }

        //private static string serverHostName = "192.168.100.40";
        //    private static string port = "5432";
        //    private static string userID = "postgres";
        //    private static string password = "admin";
        //    private static string dataBaseName = "SICPADylanJaramilloDB";

        //String connection
        public static NpgsqlConnection GetMySQLConnection()
        {
            //Always put IP server in the string connection
            //return new NpgsqlConnection("Server=" + serverHostName + ";Port=" + port + ";User Id=" + userID + ";Password=" + password +
            //    ";Database=" + dataBaseName + ";");

            completeConnection = "";

            completeConnection = "Server=" + serverHostName.Trim() + ";Port=" + port.Trim() + ";User Id=" + userID.Trim() + ";Password=" + password.Trim() +
               ";Database=" + dataBaseName.Trim() + ";";

            return new NpgsqlConnection(completeConnection.Trim());
        }


        public static void TestConnnection(ref string result)
        {

            //Try to confirm if exists connection to the database

            result = string.Empty;

            NpgsqlConnection con = GetMySQLConnection();

            try
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    result = "Connection to " + dataBaseName + " was successful.";
                }
                else
                {
                    result = "Connection to " + dataBaseName + " was a fail.";
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
}