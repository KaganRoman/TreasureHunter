using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

using Beacoun.Touch;
using CoreLocation;
using Xamarin.Forms;
using Microsoft.AspNet.SignalR.Client;

namespace BeaconTest.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			// Code for starting up the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
			#endif

			var rangeDelegate = DependencyService.Get<IBeaconReceiver> () as BeaconRangeDelegate;
			rangeDelegate.UserId = AdSupport.ASIdentifierManager.SharedManager.AdvertisingIdentifier.AsString ();

			var appId = "7ca3ad0cf7904952a675a645a00b9c51";
			ROXIMITYEngine.StartWithLaunchOptions (options, null, appId, new BeaconServiceTouch());
			ROXIMITYEngine.SetBeaconRangeDelegate(rangeDelegate, ROXBeaconRangeUpdateInterval.Fastest);

			var r = new CLLocationManager ();
			r.RequestAlwaysAuthorization ();

			LoadApplication (new App ());

			var res = base.FinishedLaunching (app, options);

			return res;
		}

		public override void OnResignActivation (UIApplication uiApplication)
		{
			ROXIMITYEngine.ResignActive ();
			base.OnResignActivation (uiApplication);
		}

		public override void OnActivated (UIApplication uiApplication)
		{
			base.OnActivated (uiApplication);
			ROXIMITYEngine.Active ();
		}

		public override void DidEnterBackground (UIApplication uiApplication)
		{
			ROXIMITYEngine.Background ();
			base.DidEnterBackground (uiApplication);
		}

		public override void WillEnterForeground (UIApplication uiApplication)
		{
			base.WillEnterForeground (uiApplication);
			ROXIMITYEngine.Foreground ();
		}

		public override void RegisteredForRemoteNotifications (UIApplication application, NSData deviceToken)
		{
			ROXIMITYEngine.DidRegisterForRemoteNotifications (deviceToken);
		}

		public override void FailedToRegisterForRemoteNotifications (UIApplication application, NSError error)
		{
			ROXIMITYEngine.DidFailToRegisterForRemoteNotifications (error);
		}

		public override void DidReceiveRemoteNotification (UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
		{
		}
	}
}

