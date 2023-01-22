using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.AppCompat.App;
using Org.Apache.Commons.Logging;
using SICPADylanJaramilloApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SICPADylanJaramilloApp
{
    [Activity(Label = "SICPA Dylan Jaramillo App", Theme = "@style/AppTheme", MainLauncher = false)]
    internal class DepartmentsActivity : AppCompatActivity
    {
        //Variables
        List<DepartmentsList> _departmentsList = new List<DepartmentsList>();
        DepartmentsList _departmentsObject = new DepartmentsList();
        List<string> enterprisesList = new List<string>();
        MyTask task;
        string result = "";
        bool _answer;
        public string spinnerEnterprise, spinnerEditStatusDepartment, spinnerEditDepEnterprise = "";
        public static string deviceName = "";


        //Controls
        ListView _lsvDepartments;
        EditText _etDepartmentsSearch, _etName, _etDescription, _etPhone;
        ImageButton _btnDepartmentsSearch, _btnAddDepartments;
        Button _btnCancelAddDepartments, _btnAddingDepartments, _btnCancelEditDepartments, _btnEditDepartments;
        Spinner _spnEnterprises, _spnStatus, _spnEditEnterprises;
        TextView _tvDepartmentsEdit;









        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_departments);


            deviceName = Intent.GetStringExtra("deviceName");







            _lsvDepartments = FindViewById<ListView>(Resource.Id.lsvDepartments);
            _lsvDepartments.ItemLongClick += _lsvDepartments_ItemLongClick;
            _etDepartmentsSearch = FindViewById<EditText>(Resource.Id.etDepartmentsSearch);
            _etDepartmentsSearch.EditorAction += _etDepartmentsSearch_EditorAction;
            _btnDepartmentsSearch = FindViewById<ImageButton>(Resource.Id.btnDepartmentsSearch);
            _btnDepartmentsSearch.Click += _btnDepartmentsSearch_Click; 
            _btnAddDepartments = FindViewById<ImageButton>(Resource.Id.btnAddDepartments);
            _btnAddDepartments.Click += _btnAddDepartments_Click;




            //Execute all data functions
            task = new MyTask(this);
            task.Execute();


        }



        [Obsolete]
        public class MyTask : AsyncTask
        {
            public DepartmentsActivity context;
            [Obsolete]
            private ProgressDialog progress;

            [Obsolete]
            public MyTask(DepartmentsActivity context)
            {
                this.context = context;
            }

            [Obsolete]
            protected override void OnPreExecute()
            {
                progress = new ProgressDialog(context);
                progress.Indeterminate = true;
                progress.SetProgressStyle(ProgressDialogStyle.Spinner);
                progress.SetMessage("Loading Departments Information");
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
                    context._departmentsList = Departments.GetDepartmentsInfo(ref context.result, context._etDepartmentsSearch.Text.Trim());


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
                    if (context._departmentsList.Count > 0)
                    {

                        context._lsvDepartments.Adapter = new DepartmentsListAdapter(context, context._departmentsList);
                        context._lsvDepartments.ChoiceMode = ChoiceMode.Single;

                    }
                    else
                    {
                        context._lsvDepartments.Adapter = null;
                        Toast.MakeText(context, "No department found.", ToastLength.Long).Show();
                    }
                }
                catch (Exception ex)
                {
                    string message = new StringReader(ex.Message.ToString()).ReadLine();
                    Toast.MakeText(context, message, ToastLength.Long).Show();

                }

            }
        }


        private void _btnAddDepartments_Click(object sender, EventArgs e)
        {
            //Hide keyboard when then finish the search
            HideKeyboard();

            ShowCustomDialogAddDepartments(this);
        }

        private void _btnDepartmentsSearch_Click(object sender, EventArgs e)
        {
            //Hide keyboard when then finish the search
            HideKeyboard();


            //Get information from departments tale
            GetDepartmentsInfo(_etDepartmentsSearch.Text.ToLower().Trim());
        }

        private void _etDepartmentsSearch_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            if (e.ActionId == ImeAction.Next || e.ActionId == ImeAction.Done)
            {
                InputMethodManager inputManager = (InputMethodManager)GetSystemService(Android.Content.Context.InputMethodService);
                var currentFocus = Window.CurrentFocus;

                if (currentFocus != null)
                {
                    inputManager.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
                }



                //Get information from departments tale
                GetDepartmentsInfo(_etDepartmentsSearch.Text.ToLower().Trim());
            }
        }

        private void _lsvDepartments_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            int selectedItem;
            selectedItem = e.Position;
            _departmentsObject = new DepartmentsList();

            //I select the item in the actual list of enterprises
            _departmentsObject = _departmentsList[selectedItem];


            ShowCustomDialogEditDepartments(this);
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


        private void GetDepartmentsInfo(string departmentsInfo)
        {
            try
            {
                //Get information from departments tale
                _departmentsList = Departments.GetDepartmentsInfo(ref result, departmentsInfo);


                if (_departmentsList.Count > 0)
                {

                    _lsvDepartments.Adapter = new DepartmentsListAdapter(this, _departmentsList);
                    _lsvDepartments.ChoiceMode = ChoiceMode.Single;

                }
                else
                {
                    _lsvDepartments.Adapter = null;
                    Toast.MakeText(this, "No departments found.", ToastLength.Long).Show();
                }
            }
            catch (Exception ex)
            {
                string message = new StringReader(ex.Message.ToString()).ReadLine();
                Toast.MakeText(this, message, ToastLength.Long).Show();
            }


        }


        private void ShowCustomDialogAddDepartments(Android.Content.Context context)
        {

            Dialog dialog = new Dialog(context);
            dialog.SetCancelable(false);
            dialog.SetContentView(Resource.Layout.activity_add_departments);



            _etName = (EditText)dialog.FindViewById(Resource.Id.etName);
            _etDescription = (EditText)dialog.FindViewById(Resource.Id.etDescription);
            _etPhone = (EditText)dialog.FindViewById(Resource.Id.etPhone);
            _spnEnterprises = (Spinner)dialog.FindViewById(Resource.Id.spnEnterprises);


            _btnAddingDepartments = (Button)dialog.FindViewById(Resource.Id.btnAddDepartments);
            _btnCancelAddDepartments = (Button)dialog.FindViewById(Resource.Id.btnCancelAddDepartments);

            AddSpinnerData(_spnEnterprises);


            dialog.Show();

            _btnAddingDepartments.Click += _btnAddingDepartments_Click;
            _btnCancelAddDepartments.Click += _btnCancelAddDepartments_Click;
            _spnEnterprises.ItemSelected += _spnEnterprises_ItemSelected;



            void _spnEnterprises_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
            {
                Spinner spinner = (Spinner)sender;
                spinnerEnterprise = string.Format("{0}", spinner.GetItemAtPosition(e.Position));



            }


            void _btnCancelAddDepartments_Click(object sender, EventArgs e)
            {
                CleanVariables();
                dialog.Dismiss();
            }

            void _btnAddingDepartments_Click(object sender, EventArgs e)
            {
                result = "";
                int answer;

                if (string.IsNullOrEmpty(_etName.Text.Trim()) || string.IsNullOrEmpty(_etDescription.Text.Trim()) || string.IsNullOrEmpty(_etPhone.Text.Trim()) 
                    || string.IsNullOrEmpty(spinnerEnterprise.Trim()) )
                {
                    Toast.MakeText(this, "Dont let any fields blank.", ToastLength.Short).Show();
                    return;
                }
                else
                {
                    try
                    {
                        //Confirm if the departments already exists
                        Departments.ConfirmExistingDepartments(ref result, _etName.Text.ToLower().Trim(), spinnerEnterprise.ToLower().Trim());

                        if (result == "EXIST")
                        {
                            Toast.MakeText(this, "The department or realtionship with enterprise entered already exist.", ToastLength.Short).Show();
                            return;
                        }
                        else if (result == "NOT EXIST")
                        {
                            result = "";
                            //Insert the informaction entered to the database
                            Departments.InsertUpdateDeleteDepartments(ref result, "INSERT", _etName.Text.Trim(), _etDescription.Text.Trim(), _etPhone.Text.Trim(),
                                '1', deviceName.Trim(), 0, spinnerEnterprise.ToLower().Trim());


                            _answer = int.TryParse(result, out answer);

                            if (_answer == true)
                            {

                                CleanVariables();
                                dialog.Dismiss();
                                Toast.MakeText(this, "The department added with success.", ToastLength.Short).Show();
                                GetDepartmentsInfo("");
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
                _etDescription.Text = "";
                _etPhone.Text = "";
                _etDepartmentsSearch.Text = "";
            }


        }

        void AddSpinnerData(Spinner spinner)
        {
            result = "";
            try
            {

                enterprisesList = Departments.GetEnterprisesName(ref result);
                ArrayAdapter<String> array = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, enterprisesList);
                spinner.Adapter = array;
            }
            catch (Exception ex)
            {

                string message = new StringReader(ex.Message.ToString()).ReadLine();
                Toast.MakeText(this, message, ToastLength.Long).Show();

            }
        }


        private void ShowCustomDialogEditDepartments(Android.Content.Context context)
        {

            Dialog dialog = new Dialog(context);
            dialog.SetCancelable(false);
            dialog.SetContentView(Resource.Layout.activity_edit_departments);



            _etName = (EditText)dialog.FindViewById(Resource.Id.etName);
            _etDescription = (EditText)dialog.FindViewById(Resource.Id.etDescription);
            _etPhone = (EditText)dialog.FindViewById(Resource.Id.etPhone);
            _spnStatus = (Spinner)dialog.FindViewById(Resource.Id.spnStatus);
            _spnEditEnterprises = (Spinner)dialog.FindViewById(Resource.Id.spnEditDepEnterprise);
            _tvDepartmentsEdit = (TextView)dialog.FindViewById(Resource.Id.tvDepartmentEdit);

            _btnEditDepartments = (Button)dialog.FindViewById(Resource.Id.btnEditionDepartments);
            _btnCancelEditDepartments = (Button)dialog.FindViewById(Resource.Id.btnCancelEditDepartments);


            AddSpinnerStatusData(_spnStatus);
            AddSpinnerData(_spnEditEnterprises);

            FillEditField(_departmentsObject.name, _departmentsObject.description, _departmentsObject.phone, _departmentsObject.status,
                _departmentsObject.enterpriseName);




            dialog.Show();

            _btnEditDepartments.Click += _btnEditDepartments_Click; ;
            _btnCancelEditDepartments.Click += _btnCancelEditDepartments_Click; ;
            _spnStatus.ItemSelected += _spnStatus_ItemSelected;
            _spnEditEnterprises.ItemSelected += _spnEditEnterprises_ItemSelected;


            void _btnCancelEditDepartments_Click(object sender, EventArgs e)
            {
                CleanVariables();
                dialog.Dismiss();
            }

            void _btnEditDepartments_Click(object sender, EventArgs e)
            {
                result = "";
                int answer;

                if (string.IsNullOrEmpty(_etName.Text.Trim()) || string.IsNullOrEmpty(_etDescription.Text.Trim()) || string.IsNullOrEmpty(_etPhone.Text.Trim())
                    || string.IsNullOrEmpty(spinnerEditDepEnterprise.Trim()))
                {
                    Toast.MakeText(this, "Dont let any fields blank.", ToastLength.Short).Show();
                    return;
                }
                else
                {
                    try
                    {
                        //Confirm if the enterprise already exists
                        Departments.ConfirmEditExistingDepartment(ref result, _etName.Text.ToLower().Trim(), _departmentsObject.id, spinnerEditDepEnterprise.ToLower().Trim());

                        if (result == "EXIST")
                        {
                            Toast.MakeText(this, "The department or realtionship with enterprise entered already exist.", ToastLength.Short).Show();
                            return;
                        }
                        else if (result == "NOT EXIST")
                        {
                            result = "";
                            //Insert the informaction entered to the database
                            Departments.InsertUpdateDeleteDepartments(ref result, "UPDATE", _etName.Text.Trim(), _etDescription.Text.Trim(), _etPhone.Text.Trim(),
                               Convert.ToChar(spinnerEditStatusDepartment.Trim()), deviceName.Trim(), _departmentsObject.id, spinnerEditDepEnterprise.ToLower().Trim());


                            _answer = int.TryParse(result, out answer);

                            if (_answer == true)
                            {

                                CleanVariables();
                                dialog.Dismiss();
                                Toast.MakeText(this, "The department modified with success.", ToastLength.Short).Show();
                                GetDepartmentsInfo("");
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
                spinnerEditStatusDepartment = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

                if (spinnerEditStatusDepartment == "Active")
                {
                    spinnerEditStatusDepartment = "1";
                }
                else
                {
                    spinnerEditStatusDepartment = "0";
                }
            }


            void CleanVariables()
            {
                _etName.Text = "";
                _etDescription.Text = "";
                _etPhone.Text = "";
                _etDepartmentsSearch.Text = "";
                _tvDepartmentsEdit.Text = "";
            }


            void AddSpinnerStatusData(Spinner spinner)
            {
                string[] spinnerData = { "Active", "Inactive" };
                ArrayAdapter<String> array = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, spinnerData);
                spinner.Adapter = array;
            }

            void FillEditField(string name, string description, string phone, string status, string enterprise)
            {
                //Filling the fields with the information of selected enterprise
                _etName.Text = name;
                _etDescription.Text = description;
                _etPhone.Text = phone;
                _tvDepartmentsEdit.Text = "Editing \"" + name + "\" department:";


                if (status == "Active")
                {
                    _spnStatus.SetSelection(0);
                }
                else
                {
                    _spnStatus.SetSelection(1);
                }

                _spnEditEnterprises.SetSelection(enterprisesList.IndexOf(enterprise));







            }


            void _spnEditEnterprises_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
            {
                Spinner spinner = (Spinner)sender;
                spinnerEditDepEnterprise = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

              
            }

        }

    }
}