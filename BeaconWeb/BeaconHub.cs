using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace BeaconWeb
{
	[HubName("beaconHub2")]
	public class BeaconHub : Hub
	{
		public void updateUsersPosition(Dictionary<string, int> message)
		{
			Clients.All.updateUsersPosition(message);
		}

		public void updateServer(object message)
		{
			Clients.All.updateServer(message);
		}
	}
}
