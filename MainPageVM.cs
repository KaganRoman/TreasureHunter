using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BeaconTest
{
	public class MainPageVM : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
			
		private IBeaconReceiver _beaconReceiver;

		private IEnumerable<BeaconStatus> _beaconsStatus;

		public MainPageVM ()
		{
			_beaconReceiver = DependencyService.Get<IBeaconReceiver> ();
			_beaconReceiver.BeaconsUpdated += (sender, e) => BeaconsStatus = e;
			_beaconsStatus = new List<BeaconStatus> ();
		}

		public IEnumerable<BeaconStatus> BeaconsStatus 
		{ 
			get { return _beaconsStatus; }
			set {
				_beaconsStatus = value;
				OnPropertyChange ();
			}
		}

		private void OnPropertyChange([CallerMemberName] string name = null)
		{
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(name));
			}
		}
	}
}

