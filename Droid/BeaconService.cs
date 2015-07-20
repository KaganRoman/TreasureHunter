using System;
using BeaconTest.Droid;

[assembly: Xamarin.Forms.Dependency (typeof (BeaconService))]
namespace BeaconTest.Droid
{
	public class BeaconService : IBeaconReceiver
	{
		public BeaconService ()
		{
		}

		#region IBeaconReceiver implementation

		public event EventHandler<System.Collections.Generic.IEnumerable<BeaconStatus>> BeaconsUpdated;

		public string UserId {
			get {
				throw new NotImplementedException ();
			}
		}

		#endregion
	}
}

