using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Com.Roximity.Sdk;

namespace BeaconTest.Droid
{
	[Activity (Label = "BeaconTest.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, IROXIMITYEngineListener
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			ROXIMITYEngine.StartEngineWithOptions(this.ApplicationContext, Resource.Drawable.notification_template_icon_bg, null, this, null);


			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App ());
		}

		public void OnROXIMITYEngineStarted ()
		{
		}

		public void OnROXIMITYEngineStopped ()
		{
		}
	}
}

