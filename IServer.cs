using System;
using System.Collections.Generic;

namespace BeaconTest
{
	public interface IServer
	{
		event EventHandler<Dictionary<string, int>> PositionsUpdated;
		void UpdateServer(object message);
	}
}

