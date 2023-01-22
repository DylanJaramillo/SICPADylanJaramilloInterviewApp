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
    public class EmployeesList
    {

        public int id { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string modifiedBy { get; set; }
        public DateTime modifiedDate { get; set; }
        public string status { get; set; }

        public int age { get; set; }

        public string email { get; set; }
        public string name { get; set; }

        public string position { get; set; }
        public string surname { get; set; }

      

    }
}