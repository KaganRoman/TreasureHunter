using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Com.Roximity.Sdk;
using Com.Roximity.Sdk.External;

namespace BeaconTest.Droid
{
	[Activity (Label = "BeaconTest.Droid", Icon = "@drawable/icon", MainLauncher = true, 
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, IROXIMITYEngineListener
	{
		private BeaconBrodcast _receiver;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			var c = new Com.Loopj.Android.Http.AsyncHttpClient ();

			ROXIMITYEngine.StartEngineWithOptions(this.ApplicationContext, Resource.Drawable.notification_template_icon_bg, null, this, null);
			_receiver = new BeaconBrodcast ();
			var intentFilter = new IntentFilter ();
			intentFilter.AddAction (ROXConsts.MessageFired);
			intentFilter.AddAction (ROXConsts.BeaconRangeUpdate);
			RegisterReceiver (_receiver, intentFilter);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App ());
		}

		public void OnROXIMITYEngineStarted ()
		{
		}

		public void OnROXIMITYEngineStopped ()
		{
		}

		protected override void OnResume()
		{
			base.OnResume();
			RegisterReceiver(_receiver, new IntentFilter(ROXConsts.MessageFired));
			RegisterReceiver(_receiver, new IntentFilter(ROXConsts.BeaconRangeUpdate));
		}

		protected override void OnPause()
		{
			base.OnPause();
			UnregisterReceiver(_receiver);
		}
	}
}

