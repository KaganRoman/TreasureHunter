using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace BeaconWeb
{
	[HubName("beaconHub")]
	public class BeaconHub : Hub
	{
		public void updateUsers(string name, string message)
		{
			Clients.All.updateUsers(name, message);
		}

		public void updateServer(string name, string message)
		{
			Clients.All.updateServer(name, message);
		}
	}
}
