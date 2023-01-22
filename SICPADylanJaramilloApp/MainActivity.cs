using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using SICPADylanJaramilloApp.Models;
using System;
using System.Data;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using static Android.Bluetooth.BluetoothClass;

namespace SICPADylanJaramilloApp
{
    [Activity(Label = "SICPA Dylan Jaramillo App", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {
        //Variables
        string testConnectionResult = string.Empty;
        public static string deviceName = "";
        MyTask task;


        //Controls
        Button _btnEnterprise, _btnDepartments, _btnEmployees, _btnCancelConnection, _btnSaveConnection;
        ImageButton _ibDBConnection;
        EditText _etServer, _etPort, _etUserId, _etPassword, _etDataBase;




        protected override void OnCreate(Bundle savedInstanceState)
        {
            AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);




            _btnEnterprise = FindViewById<Button>(Resource.Id.btnEnterprises);
            _btnEnterprise.Click += _btnEnterprise_Click;
            _btnDepartments = FindViewById<Button>(Resource.Id.btnDepartments);
            _btnDepartments.Click += _btnDepartments_Click;
            _btnEmployees = FindViewById<Button>(Resource.Id.btnEmployees);
            _btnEmployees.Click += _btnEmployees_Click;
            _ibDBConnection = FindViewById<ImageButton>(Resource.Id.ibOpenConnection);
            _ibDBConnection.Click += _ibDBConnection_Click;



            ConfigureConnection();
            task = new MyTask(this);
            task.Execute();



            //First of all, I need to confirm if exists connection to the database with this function
            DBConnection.TestConnnection(ref testConnectionResult);

            Toast.MakeText(this, testConnectionResult, ToastLength.Long).Show();







            //Execute all data functions
            task = new MyTask(this);
            task.Execute();






        }

        private void _ibDBConnection_Click(object sender, EventArgs e)
        {
            ShowCustomDialogDBConnection(this);
           
        }

        private void _btnEmployees_Click(object sender, EventArgs e)
        {
            //Open the new page
            Intent intent = new Intent(this, typeof(EmployeesActivity));
            intent.PutExtra("deviceName", deviceName.Trim());
            StartActivityForResult(intent, 10);
        }

        private void _btnDepartments_Click(object sender, EventArgs e)
        {
            //Open the new page
            Intent intent = new Intent(this, typeof(DepartmentsActivity));
            intent.PutExtra("deviceName", deviceName.Trim());
            StartActivityForResult(intent, 10);
        }

        private void _btnEnterprise_Click(object sender, System.EventArgs e)
        {
            //Open the new page
            Intent intent = new Intent(this, typeof(EnterprisesActivity));
            intent.PutExtra("deviceName", deviceName.Trim());
            StartActivityForResult(intent, 10);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        [Obsolete]
        public class MyTask : AsyncTask
        {
            public MainActivity context;
            [Obsolete]
            private ProgressDialog progress;

            [Obsolete]
            public MyTask(MainActivity context)
            {
                this.context = context;
            }

            [Obsolete]
            protected override void OnPreExecute()
            {
                progress = new ProgressDialog(context);
                progress.Indeterminate = true;
                progress.SetProgressStyle(ProgressDialogStyle.Spinner);
                progress.SetMessage("Loading Information.");
                progress.SetCancelable(false);
                progress.Show();
            }

            [Obsolete]
            protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
            {
                //Getting the name of the device for audit
                Android.Bluetooth.BluetoothAdapter m_BluetoothAdapter = Android.Bluetooth.BluetoothAdapter.DefaultAdapter;
                deviceName = m_BluetoothAdapter.Name;

                progress.Dismiss();

                return 0;
            }






        }



        public string ConfigureConnection()
        {
            string status = "";
            try
            {
                string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                string filename = Path.Combine(path, "postgreSQLconnection.txt");
                Java.IO.File file = new Java.IO.File(filename);

                if (file.Exists())
                {
                    using (var streamReader = new StreamReader(filename))
                    {
                        string content = streamReader.ReadToEnd();
                        System.Diagnostics.Debug.WriteLine(content);

                        string[] dataBaseConnectionInfo;
                        dataBaseConnectionInfo = content.Trim().ToString().Split(';');


                        DBConnection.serverHostName = dataBaseConnectionInfo[0].Replace("Server=", "").Trim();
                        DBConnection.port = dataBaseConnectionInfo[1].Replace("Port=", "").Trim();
                        DBConnection.userID = dataBaseConnectionInfo[2].Replace("User Id=", "").Trim();
                        DBConnection.password = dataBaseConnectionInfo[3].Replace("Password=", "").Trim();
                        DBConnection.dataBaseName = dataBaseConnectionInfo[4].Replace("Database=", "").Trim();
                    }
                }
                else
                {
                    //Default data connection
                    using (var streamWriter = new StreamWriter(filename, false))
                    {
                        streamWriter.WriteLine(@"Server=192.168.100.40;");
                        streamWriter.WriteLine(@"Port=5432;");
                        streamWriter.WriteLine(@"User Id=postgres;");
                        streamWriter.WriteLine(@"Password=admin;");
                        streamWriter.WriteLine(@"Database=SICPADylanJaramilloDB;");
                    }

                    using (var streamReader = new StreamReader(filename))
                    {
                        string content = streamReader.ReadToEnd();
                        System.Diagnostics.Debug.WriteLine(content);

                        string[] dataBaseConnectionInfo;
                        dataBaseConnectionInfo = content.Trim().ToString().Split(';');


                        DBConnection.serverHostName = dataBaseConnectionInfo[0].Replace("Server=", "").Trim();
                        DBConnection.port = dataBaseConnectionInfo[1].Replace("Port=", "").Trim();
                        DBConnection.userID = dataBaseConnectionInfo[2].Replace("User Id=", "").Trim();
                        DBConnection.password = dataBaseConnectionInfo[3].Replace("Password=", "").Trim();
                        DBConnection.dataBaseName = dataBaseConnectionInfo[4].Replace("Database=", "").Trim();







                    }
                }
            }
            catch (Exception ex)
            {
                string message = new StringReader(ex.Message.ToString()).ReadLine();
                Toast.MakeText(this, message, ToastLength.Long).Show();
            }
            return status;
        }




        private void ShowCustomDialogDBConnection(Android.Content.Context context)
        {

            Dialog dialog = new Dialog(context);
            dialog.SetCancelable(false);
            dialog.SetContentView(Resource.Layout.activity_db_connection);




            _btnCancelConnection = (Button)dialog.FindViewById(Resource.Id.btnCancelConnection);
            _btnSaveConnection = (Button)dialog.FindViewById(Resource.Id.btnSaveConnection);
            _btnCancelConnection.Click += _btnCancelConnection_Click;
            _btnSaveConnection.Click += _btnSaveConnection_Click;

            _etServer = (EditText)dialog.FindViewById(Resource.Id.etServer);
            _etPort = (EditText)dialog.FindViewById(Resource.Id.etPort);
            _etUserId = (EditText)dialog.FindViewById(Resource.Id.etUserId);
            _etPassword = (EditText)dialog.FindViewById(Resource.Id.etPassword);
            _etDataBase = (EditText)dialog.FindViewById(Resource.Id.etDDataBase);



            FillEditField(DBConnection.serverHostName, DBConnection.port, DBConnection.userID, DBConnection.password, DBConnection.dataBaseName);


            dialog.Show();




            void _btnCancelConnection_Click(object sender, EventArgs e)
            {
                CleanVariables();
                dialog.Dismiss();
            }

            void _btnSaveConnection_Click(object sender, EventArgs e)
            {
               
               

                if (string.IsNullOrEmpty(_etServer.Text.Trim()) || string.IsNullOrEmpty(_etPort.Text.Trim()) || string.IsNullOrEmpty(_etUserId.Text.Trim())
                    || string.IsNullOrEmpty(_etPassword.Text.Trim()) || string.IsNullOrEmpty(_etDataBase.Text.Trim()))
                {
                    Toast.MakeText(this, "Dont let any fields blank.", ToastLength.Short).Show();
                    return;
                }
                else
                {
                    try
                    {


                        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                        string filename = Path.Combine(path, "postgreSQLconnection.txt");
                        Java.IO.File file = new Java.IO.File(filename);

                        if (file.Exists())
                        {
                            using (var streamWriter = new StreamWriter(filename, false))
                            {
                                streamWriter.WriteLine(@"Server=" + _etServer.Text.Trim() + ";");
                                streamWriter.WriteLine(@"Port=" + _etPort.Text.Trim() + ";");
                                streamWriter.WriteLine(@"User Id=" + _etUserId.Text.Trim() + ";");
                                streamWriter.WriteLine(@"Password=" + _etPassword.Text.Trim() + ";");
                                streamWriter.WriteLine(@"Database=" + _etDataBase.Text.Trim() + ";");

                            }

                            using (var streamReader = new StreamReader(filename))
                            {
                                string content = streamReader.ReadToEnd();
                                System.Diagnostics.Debug.WriteLine(content);


                                string[] dataBaseConnectionInfo;
                                dataBaseConnectionInfo = content.Trim().ToString().Split(';');


                                DBConnection.serverHostName = dataBaseConnectionInfo[0].Replace("Server=", "").Trim();
                                DBConnection.port = dataBaseConnectionInfo[1].Replace("Port=", "").Trim();
                                DBConnection.userID = dataBaseConnectionInfo[2].Replace("User Id=", "").Trim();
                                DBConnection.password = dataBaseConnectionInfo[3].Replace("Password=", "").Trim();
                                DBConnection.dataBaseName = dataBaseConnectionInfo[4].Replace("Database=", "").Trim();


                                Toast.MakeText(this, "Database info saved successfully", ToastLength.Short).Show();


                                //First of all, I need to confirm if exists connection to the database with this function
                                DBConnection.TestConnnection(ref testConnectionResult);

                                Toast.MakeText(this, testConnectionResult, ToastLength.Long).Show();

                                CleanVariables();
                                dialog.Dismiss();
                            }
                        }
                        else
                        {
                            Toast.MakeText(this, "Database cant saved.", ToastLength.Short).Show();
                        }




                    }
                    catch (Exception ex)
                    {
                        string message = new StringReader(ex.Message.ToString()).ReadLine();
                        Toast.MakeText(this, message, ToastLength.Short).Show();
                    }



                }
            }




            void CleanVariables()
            {
                _etServer.Text = "";
                _etPort.Text = "";
                _etUserId.Text = "";
                _etPassword.Text = "";
                _etDataBase.Text = "";

            }




            void FillEditField(string server, string port, string userID, string paswword, string database)
            {
                //Filling the fields with the information of database info
                _etServer.Text = server;
                _etPort.Text = port;
                _etUserId.Text = userID;
                _etPassword.Text = paswword;
                _etDataBase.Text = database;







            }




        }

    }
}