
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Roximity.Sdk.External;

namespace BeaconTest.Droid
{
	[BroadcastReceiver]
	[IntentFilter(new [] { ROXConsts.MessageFired, ROXConsts.BeaconRangeUpdate })]
	public class BeaconBrodcast : BroadcastReceiver
	{
		public override void OnReceive (Context context, Intent intent)
		{
			Toast.MakeText (context, "Received intent!", ToastLength.Short).Show ();
			if (intent.Action == ROXConsts.BeaconRangeUpdate) {
				var json = intent.GetStringExtra (ROXConsts.ExtraRangeData);
			}
		}
	}
}

