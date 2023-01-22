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
    public class DepartmentsListAdapter : BaseAdapter<DepartmentsList>
    {

        private readonly Activity _context;
        private readonly List<DepartmentsList> _list;


        public DepartmentsListAdapter(Activity context, List<DepartmentsList> items) : base()
        {
            _context = context;
            _list = items;
        }

        public override DepartmentsList this[int position] => throw new NotImplementedException();

        public override int Count => _list.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _list[position];


            if (convertView == null)
            {
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.activity_list_item_departments, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.tvNameColumn).Text = item.name;
            convertView.FindViewById<TextView>(Resource.Id.tvDescriptionColumn).Text = item.description;
            convertView.FindViewById<TextView>(Resource.Id.tvPhoneColumn).Text = item.phone;
            convertView.FindViewById<TextView>(Resource.Id.tvEnterpriseColumn).Text = item.enterpriseName;
            convertView.FindViewById<TextView>(Resource.Id.tvStatusColumn).Text = item.status;


            return convertView;
        }
    }
}