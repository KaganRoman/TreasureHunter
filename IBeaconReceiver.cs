using System;
using System.Collections.Generic;

namespace BeaconTest
{
	public class BeaconStatus
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Tags { get; set; }
		public string ProximityValue { get; set; }
		public string ProximityString { get; set; }
	}
	
	public interface IBeaconReceiver
	{
		event EventHandler<IEnumerable<BeaconStatus>> BeaconsUpdated;
	}
}

