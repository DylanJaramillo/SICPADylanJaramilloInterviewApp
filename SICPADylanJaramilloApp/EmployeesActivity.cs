using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.AppCompat.App;
using SICPADylanJaramilloApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SICPADylanJaramilloApp
{
    [Activity(Label = "SICPA Dylan Jaramillo App", Theme = "@style/AppTheme", MainLauncher = false)]
    internal class EmployeesActivity : AppCompatActivity
    {
        //Variables
        List<EmployeesList> _employeesList = new List<EmployeesList>();
        EmployeesList _employeesObject = new EmployeesList();

    
        public static string deviceName = "";







        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_employees);


            deviceName = Intent.GetStringExtra("deviceName");










          

        }



        









    }
}