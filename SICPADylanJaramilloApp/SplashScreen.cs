using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SICPADylanJaramilloApp
{
    [Activity(Label = "SICPA Dylan Jaramillo App", MainLauncher = true, Theme = "@style/Theme.Splash", NoHistory = true, Icon = "@drawable/dylisLogo")]
    public class SplashScreen : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            
            base.OnCreate(bundle);
            //Display Splash Screen for 4 Sec  
            Thread.Sleep(4000);
            //Start Activity1 Activity  
            StartActivity(typeof(MainActivity));
        }
    }
}