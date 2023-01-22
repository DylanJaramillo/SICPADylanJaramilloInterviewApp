package crc64d7cbf1c4d7291697;


public class MainActivity_MyTask
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
			"";
		mono.android.Runtime.register ("SICPADylanJaramilloApp.MainActivity+MyTask, SICPADylanJaramilloApp", MainActivity_MyTask.class, __md_methods);
	}


	public MainActivity_MyTask ()
	{
		super ();
		if (getClass () == MainActivity_MyTask.class) {
			mono.android.TypeManager.Activate ("SICPADylanJaramilloApp.MainActivity+MyTask, SICPADylanJaramilloApp", "", this, new java.lang.Object[] {  });
		}
	}

	public MainActivity_MyTask (crc64d7cbf1c4d7291697.MainActivity p0)
	{
		super ();
		if (getClass () == MainActivity_MyTask.class) {
			mono.android.TypeManager.Activate ("SICPADylanJaramilloApp.MainActivity+MyTask, SICPADylanJaramilloApp", "SICPADylanJaramilloApp.MainActivity, SICPADylanJaramilloApp", this, new java.lang.Object[] { p0 });
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
