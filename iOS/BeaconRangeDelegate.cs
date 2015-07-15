using System;
using Beacoun.Touch;
using Foundation;
using System.Collections.Generic;
using BeaconTest;
using BeaconTest.iOS;

[assembly: Xamarin.Forms.Dependency (typeof (BeaconRangeDelegate))]
namespace BeaconTest.iOS
{
	public class BeaconRangeDelegate : ROXBeaconRangeUpdateDelegate, IBeaconReceiver
	{
		public BeaconRangeDelegate()
		{
		}
		
		#region IBeaconReceiver implementation

		public event EventHandler<IEnumerable<BeaconStatus>> BeaconsUpdated;

		public string UserId { get; set; }

		#endregion
			
		public override void DidUpdateBeaconRanges (NSObject[] rangedBeacons)
		{
			if (BeaconsUpdated != null) 
			{
				var list = new List<BeaconStatus> ();
				foreach (var b in rangedBeacons) {
					var bStatus = new BeaconStatus ();
					var ndict = b as NSDictionary;
					foreach (var p in ndict) {
						if (p.Key.ToString() == "beacon_id")
							bStatus.Id = p.Value.ToString();
						if (p.Key.ToString() == "beacon_tags")
							bStatus.Tags = p.Value.ToString();
						if (p.Key.ToString() == "proximity_value")
							bStatus.ProximityValue = p.Value.ToString();
						if (p.Key.ToString() == "beacon_name")
							bStatus.Name = p.Value.ToString();
						if (p.Key.ToString() == "proximity_string")
							bStatus.ProximityString = p.Value.ToString();
					}
					list.Add (bStatus);
				}
				InvokeOnMainThread(() => BeaconsUpdated (this, list));
			}
		}
	}
}

