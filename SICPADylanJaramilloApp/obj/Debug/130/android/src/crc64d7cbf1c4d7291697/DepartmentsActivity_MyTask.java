package crc64d7cbf1c4d7291697;


public class DepartmentsActivity_MyTask
	extends android.os.AsyncTask
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPreExecute:()V:GetOnPreExecuteHandler\n" +
			"n_doInBackground:([Ljava/lang/Object;)Ljava/lang/Object;:GetDoInBackground_arrayLjava_lang_Object_Handler\n" +
			"n_onPostExecute:(Ljava/lang/Object;)V:GetOnPostExecute_Ljava_lang_Object_Handler\n" +
			"";
		mono.android.Runtime.register ("SICPADylanJaramilloApp.DepartmentsActivity+MyTask, SICPADylanJaramilloApp", DepartmentsActivity_MyTask.class, __md_methods);
	}


	public DepartmentsActivity_MyTask ()
	{
		super ();
		if (getClass () == DepartmentsActivity_MyTask.class) {
			mono.android.TypeManager.Activate ("SICPADylanJaramilloApp.DepartmentsActivity+MyTask, SICPADylanJaramilloApp", "", this, new java.lang.Object[] {  });
		}
	}

	public DepartmentsActivity_MyTask (crc64d7cbf1c4d7291697.DepartmentsActivity p0)
	{
		super ();
		if (getClass () == DepartmentsActivity_MyTask.class) {
			mono.android.TypeManager.Activate ("SICPADylanJaramilloApp.DepartmentsActivity+MyTask, SICPADylanJaramilloApp", "SICPADylanJaramilloApp.DepartmentsActivity, SICPADylanJaramilloApp", this, new java.lang.Object[] { p0 });
		}
	}


	public void onPreExecute ()
	{
		n_onPreExecute ();
	}

	private native void n_onPreExecute ();


	public java.lang.Object doInBackground (java.lang.Object[] p0)
	{
		return n_doInBackground (p0);
	}

	private native java.lang.Object n_doInBackground (java.lang.Object[] p0);


	public void onPostExecute (java.lang.Object p0)
	{
		n_onPostExecute (p0);
	}

	private native void n_onPostExecute (java.lang.Object p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
