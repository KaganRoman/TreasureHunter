using System;

using Beacoun.Touch;
using Foundation;
using CoreBluetooth;
using CoreLocation;

namespace BeaconTest.iOS
{
	public class BeaconServiceTouch : ROXIMITYEngineDelegate
	{
		public override void AliasRemoveResult (bool success, NSError error)
		{
		}

		public override void AliasSetResult (bool success, string alias, NSError error)
		{
		}

		public override void BluetoothRoximityUsable (bool usable, CBCentralManagerState state)
		{
		}

		public override void LocationRoximityUsable (bool usable, CLAuthorizationStatus authStatus)
		{
		}

		public override void NotificationsRoximityPermitted (bool permitted)
		{
		}
	}
}

