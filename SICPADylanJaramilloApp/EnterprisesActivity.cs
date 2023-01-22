using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using SICPADylanJaramilloApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace SICPADylanJaramilloApp
{
    [Activity(Label = "SICPA Dylan Jaramillo App", Theme = "@style/AppTheme", MainLauncher = false)]
    internal class EnterprisesActivity : AppCompatActivity
    {

        //Variables
        List<EnterprisesList> _enterprisesList = new List<EnterprisesList>();
        EnterprisesList _enterprisesObject = new EnterprisesList();
        MyTask task;
        string result = "";
        bool _answer;
        public string spinnerStatus = "";
        public static string deviceName = "";


        //Controls
        ListView _lsvEnterprises;
        EditText _etEnterprisesSearch, _etName, _etAddres, _etPhone;
        ImageButton _btnEnterprisesSearch, _btnAddEnterprises;
        Button _btnCancelAddEnterprises, _btnAddingEnterprises, _btnCancelEditEnterprises, _btnEditEnterprises;
        Spinner _spnStatus;
        TextView _tvEnterpriseEdit;





        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_enterprises);

            deviceName = Intent.GetStringExtra("deviceName");



            _lsvEnterprises = FindViewById<ListView>(Resource.Id.lsvEnterprises);
            _lsvEnterprises.ItemLongClick += _lsvEnterprises_ItemLongClick;
            _etEnterprisesSearch = FindViewById<EditText>(Resource.Id.etEnterprisesSearch);
            _etEnterprisesSearch.EditorAction += _etEnterprisesSearch_EditorAction;
            _btnEnterprisesSearch = FindViewById<ImageButton>(Resource.Id.btnEnterprisesSearch);
            _btnEnterprisesSearch.Click += _btnEnterprisesSearch_Click;
            _btnAddEnterprises = FindViewById<ImageButton>(Resource.Id.btnAddEnterprises);
            _btnAddEnterprises.Click += _btnAddEnterprises_Click;




            //Execute all data functions
            task = new MyTask(this);
            task.Execute();



        }

        private void _btnAddEnterprises_Click(object sender, EventArgs e)
        {
            //Hide keyboard when then finish the search
            HideKeyboard();

            ShowCustomDialogAddEnterprises(this);

        }

        private void _btnEnterprisesSearch_Click(object sender, EventArgs e)
        {
            //Hide keyboard when then finish the search
            HideKeyboard();


            //Get information from enterprises tale
            GetEnterprisesInfo(_etEnterprisesSearch.Text.ToLower().Trim());
        }

        private void _etEnterprisesSearch_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {

            if (e.ActionId == ImeAction.Next || e.ActionId == ImeAction.Done)
            {
                InputMethodManager inputManager = (InputMethodManager)GetSystemService(Android.Content.Context.InputMethodService);
                var currentFocus = Window.CurrentFocus;

                if (currentFocus != null)
                {
                    inputManager.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
                }



                //Get information from enterprises tale
                GetEnterprisesInfo(_etEnterprisesSearch.Text.ToLower().Trim());
            }
        }

        private void _lsvEnterprises_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            int selectedItem;
            selectedItem = e.Position;
            _enterprisesObject = new EnterprisesList();

            //I select the item in the actual list of enterprises
            _enterprisesObject = _enterprisesList[selectedItem];


            ShowCustomDialogEditEnterprises(this);


        }



        [Obsolete]
        public class MyTask : AsyncTask
        {
            public EnterprisesActivity context;
            [Obsolete]
            private ProgressDialog progress;

            [Obsolete]
            public MyTask(EnterprisesActivity context)
            {
                this.context = context;
            }

            [Obsolete]
            protected override void OnPreExecute()
            {
                progress = new ProgressDialog(context);
                progress.Indeterminate = true;
                progress.SetProgressStyle(ProgressDialogStyle.Spinner);
                progress.SetMessage("Loading Enterprises Information");
                progress.SetCancelable(false);
                progress.Show();
            }

            [Obsolete]
            protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
            {
                context.result = "";

                try
                {
                    //Get information from enterprises tale
                    context._enterprisesList = Enterprises.GetEnterprisesInfo(ref context.result, context._etEnterprisesSearch.Text.Trim());


                }
                catch (Exception ex)
                {

                    string message = new StringReader(ex.Message.ToString()).ReadLine();
                    Toast.MakeText(context, message, ToastLength.Long).Show();

                }
                return 0;
            }

            [Obsolete]
            protected override void OnPostExecute(Java.Lang.Object result)
            {

                progress.Dismiss();

                try
                {
                    if (context._enterprisesList.Count > 0)
                    {

                        context._lsvEnterprises.Adapter = new EnterprisesListAdapter(context, context._enterprisesList);
                        context._lsvEnterprises.ChoiceMode = ChoiceMode.Single;

                    }
                    else
                    {
                        context._lsvEnterprises.Adapter = null;
                        Toast.MakeText(context, "No enterprise found.", ToastLength.Long).Show();
                    }
                }
                catch (Exception ex)
                {
                    string message = new StringReader(ex.Message.ToString()).ReadLine();
                    Toast.MakeText(context, message, ToastLength.Long).Show();

                }

            }
        }


        private void GetEnterprisesInfo(string enterpriseInfo)
        {
            try
            {
                //Get information from enterprises tale
                _enterprisesList = Enterprises.GetEnterprisesInfo(ref result, enterpriseInfo);


                if (_enterprisesList.Count > 0)
                {

                    _lsvEnterprises.Adapter = new EnterprisesListAdapter(this, _enterprisesList);
                    _lsvEnterprises.ChoiceMode = ChoiceMode.Single;

                }
                else
                {
                    _lsvEnterprises.Adapter = null;
                    Toast.MakeText(this, "No enterprise found.", ToastLength.Long).Show();
                }
            }
            catch (Exception ex)
            {
                string message = new StringReader(ex.Message.ToString()).ReadLine();
                Toast.MakeText(this, message, ToastLength.Long).Show();
            }


        }

        private void HideKeyboard()
        {
            InputMethodManager inputManager = (InputMethodManager)GetSystemService(Android.Content.Context.InputMethodService);
            var currentFocus = Window.CurrentFocus;

            if (currentFocus != null)
            {
                inputManager.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
            }
        }


        private void ShowCustomDialogAddEnterprises(Android.Content.Context context)
        {

            Dialog dialog = new Dialog(context);
            dialog.SetCancelable(false);
            dialog.SetContentView(Resource.Layout.activity_add_enterprises);



            _etName = (EditText)dialog.FindViewById(Resource.Id.etName);
            _etAddres = (EditText)dialog.FindViewById(Resource.Id.etAddress);
            _etPhone = (EditText)dialog.FindViewById(Resource.Id.etPhone);


            _btnAddingEnterprises = (Button)dialog.FindViewById(Resource.Id.btnAddEnterprises);
            _btnCancelAddEnterprises = (Button)dialog.FindViewById(Resource.Id.btnCancelAddEnterprises);

            dialog.Show();

            _btnAddingEnterprises.Click += _btnAddingEnterprises_Click;
            _btnCancelAddEnterprises.Click += _btnCancelAddEnterprises_Click;




            void _btnCancelAddEnterprises_Click(object sender, EventArgs e)
            {
                CleanVariables();
                dialog.Dismiss();
            }

            void _btnAddingEnterprises_Click(object sender, EventArgs e)
            {
                result = "";
                int answer;

                if (string.IsNullOrEmpty(_etName.Text.Trim()) || string.IsNullOrEmpty(_etAddres.Text.Trim()) || string.IsNullOrEmpty(_etPhone.Text.Trim()))
                {
                    Toast.MakeText(this, "Dont let any fields blank.", ToastLength.Short).Show();
                    return;
                }
                else
                {
                    try
                    {
                        //Confirm if the enterprise already exists
                        Enterprises.ConfirmExistingEnterprise(ref result, _etName.Text.ToLower().Trim());

                        if (result == "EXIST")
                        {
                            Toast.MakeText(this, "The enterprise entered already exist.", ToastLength.Short).Show();
                            return;
                        }
                        else if (result == "NOT EXIST")
                        {
                            result = "";
                            //Insert the informaction entered to the database
                            Enterprises.InsertUpdateDeleteEnterprise(ref result, "INSERT", _etName.Text.Trim(), _etAddres.Text.Trim(), _etPhone.Text.Trim(),
                                '1', deviceName.Trim(), 0);


                            _answer = int.TryParse(result, out answer);

                            if (_answer == true)
                            {

                                CleanVariables();
                                dialog.Dismiss();
                                Toast.MakeText(this, "The enterprise added with success.", ToastLength.Short).Show();
                                GetEnterprisesInfo("");
                            }
                            else
                            {
                                Toast.MakeText(this, result, ToastLength.Short).Show();
                                return;
                            }





                        }
                        else
                        {
                            Toast.MakeText(this, result, ToastLength.Short).Show();
                            return;
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
                _etName.Text = "";
                _etAddres.Text = "";
                _etPhone.Text = "";
                _etEnterprisesSearch.Text = "";
            }


        }




        private void ShowCustomDialogEditEnterprises(Android.Content.Context context)
        {

            Dialog dialog = new Dialog(context);
            dialog.SetCancelable(false);
            dialog.SetContentView(Resource.Layout.activity_edit_enterprises);



            _etName = (EditText)dialog.FindViewById(Resource.Id.etName);
            _etAddres = (EditText)dialog.FindViewById(Resource.Id.etAddress);
            _etPhone = (EditText)dialog.FindViewById(Resource.Id.etPhone);
            _spnStatus = (Spinner)dialog.FindViewById(Resource.Id.spnStatus);
            _tvEnterpriseEdit = (TextView)dialog.FindViewById(Resource.Id.tvEnterpriseEdit);

            _btnEditEnterprises = (Button)dialog.FindViewById(Resource.Id.btnEditionEnterprises);
            _btnCancelEditEnterprises = (Button)dialog.FindViewById(Resource.Id.btnCancelEditEnterprises);


            AddSpinnerData(_spnStatus);

            FillEditField(_enterprisesObject.name, _enterprisesObject.address, _enterprisesObject.phone, _enterprisesObject.status);




            dialog.Show();

            _btnEditEnterprises.Click += _btnEditEnterprises_Click;
            _btnCancelEditEnterprises.Click += _btnCancelEditEnterprises_Click;
            _spnStatus.ItemSelected += _spnStatus_ItemSelected;


            void _btnCancelEditEnterprises_Click(object sender, EventArgs e)
            {
                CleanVariables();
                dialog.Dismiss();
            }

            void _btnEditEnterprises_Click(object sender, EventArgs e)
            {
                result = "";
                int answer;

                if (string.IsNullOrEmpty(_etName.Text.Trim()) || string.IsNullOrEmpty(_etAddres.Text.Trim()) || string.IsNullOrEmpty(_etPhone.Text.Trim()))
                {
                    Toast.MakeText(this, "Dont let any fields blank.", ToastLength.Short).Show();
                    return;
                }
                else
                {
                    try
                    {
                        //Confirm if the enterprise already exists
                        Enterprises.ConfirmEditExistingEnterprise(ref result, _etName.Text.ToLower().Trim(), _enterprisesObject.id);

                        if (result == "EXIST")
                        {
                            Toast.MakeText(this, "The enterprise entered already exist.", ToastLength.Short).Show();
                            return;
                        }
                        else if (result == "NOT EXIST")
                        {
                            result = "";
                            //Insert the informaction entered to the database
                            Enterprises.InsertUpdateDeleteEnterprise(ref result, "UPDATE", _etName.Text.Trim(), _etAddres.Text.Trim(), _etPhone.Text.Trim(),
                               Convert.ToChar(spinnerStatus.Trim()) , deviceName.Trim(), _enterprisesObject.id);


                            _answer = int.TryParse(result, out answer);

                            if (_answer == true)
                            {

                                CleanVariables();
                                dialog.Dismiss();
                                Toast.MakeText(this, "The enterprise modified with success.", ToastLength.Short).Show();
                                GetEnterprisesInfo("");
                            }
                            else
                            {
                                Toast.MakeText(this, result, ToastLength.Short).Show();
                                return;
                            }





                        }
                        else
                        {
                            Toast.MakeText(this, result, ToastLength.Short).Show();
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = new StringReader(ex.Message.ToString()).ReadLine();
                        Toast.MakeText(this, message, ToastLength.Short).Show();
                    }



                }
            }


            void _spnStatus_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
            {
                Spinner spinner = (Spinner)sender;
                spinnerStatus = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

                if (spinnerStatus == "Active")
                {
                    spinnerStatus = "1";
                }
                else
                {
                    spinnerStatus = "0";
                }
            }


            void CleanVariables()
            {
                _etName.Text = "";
                _etAddres.Text = "";
                _etPhone.Text = "";
                _etEnterprisesSearch.Text = "";
                _tvEnterpriseEdit.Text = "";
            }


            void AddSpinnerData(Spinner spinner)
            {
                string[] spinnerData = { "Active", "Inactive" };
                ArrayAdapter<String> array = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, spinnerData);
                spinner.Adapter = array;
            }

            void FillEditField(string name, string address, string phone, string status)
            {
                //Filling the fields with the information of selected enterprise
                _etName.Text = name;
                _etAddres.Text = address;
                _etPhone.Text = phone;
                _tvEnterpriseEdit.Text = "Editing \"" + name + "\" enterprise:";


                if (status == "Active")
                {
                    _spnStatus.SetSelection(0);
                }
                else
                {
                    _spnStatus.SetSelection(1);
                }

               





            }



        }

      
    }
}