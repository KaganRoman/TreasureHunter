using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR.Client;
using Xamarin.Forms;
using BeaconTest;

[assembly: Xamarin.Forms.Dependency (typeof (Server))]
namespace BeaconTest
{
	public class Server : IServer
	{
		private HubConnection _hubConnection;
		private IHubProxy _hubProxy;
			
		public event EventHandler<Dictionary<string, int>> PositionsUpdated;

		public Server()
		{
			try
			{
				_hubConnection = new HubConnection ("http://rumble-beacon-test.azurewebsites.net");
				_hubProxy = _hubConnection.CreateHubProxy ("beaconHub2");
				_hubProxy.On<Dictionary<string, int>> ("updateUsersPosition", UpdateUsersFromServer);
				_hubConnection.Start ().Wait();
			}
			catch(Exception e) {
			}
		}

		public void UpdateServer(object message)
		{
			try
			{
				_hubProxy.Invoke ("updateServer", message);
			}
			catch(Exception e) {
			}
		}

		private void UpdateUsersFromServer(Dictionary<string, int> users)
		{
			Device.BeginInvokeOnMainThread(() =>
				{
					if(PositionsUpdated != null)
						PositionsUpdated(this, users);
				});
		}
	}
}

