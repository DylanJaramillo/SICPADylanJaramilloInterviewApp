using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using static Android.Content.ClipData;

namespace SICPADylanJaramilloApp.Models
{
    public class EnterprisesListAdapter : BaseAdapter<EnterprisesList>
    {
        private readonly Activity _context;
        private readonly List<EnterprisesList> _list;

        public EnterprisesListAdapter(Activity context, List<EnterprisesList> items) : base()
        {
            _context = context;
            _list = items;
        }

        public override EnterprisesList this[int position] => throw new NotImplementedException();

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
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.activity_list_item_enterprises , null);
            }

            convertView.FindViewById<TextView>(Resource.Id.tvNameColumn).Text = item.name;
            convertView.FindViewById<TextView>(Resource.Id.tvAddressColumn).Text = item.address;
            convertView.FindViewById<TextView>(Resource.Id.tvPhoneColumn).Text = item.phone;
            convertView.FindViewById<TextView>(Resource.Id.tvStatusColumn).Text = item.status;
         

            return convertView;
        }
    }
}