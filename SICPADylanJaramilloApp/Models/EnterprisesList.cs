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

namespace SICPADylanJaramilloApp.Models
{
    public class EnterprisesList
    {


        public int id { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string modifiedBy { get; set; }
        public DateTime modifiedDate { get; set; }
        public string status { get; set; }

        public string address { get; set; }
        public string name { get; set; }
        public string phone { get; set; }

    }
}