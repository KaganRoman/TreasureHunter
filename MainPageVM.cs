using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace BeaconTest
{
	public class MainPageVM : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
			
		private IBeaconReceiver _beaconReceiver;

		private IEnumerable<BeaconStatus> _beaconsStatus;

		private string _timer;
		private CancellationTokenSource _timerCancel;

		private Xamarin.Forms.Command _startCommand;
		private Xamarin.Forms.Command _stopCommand;
		private bool _started;

		public MainPageVM ()
		{
			_beaconReceiver = DependencyService.Get<IBeaconReceiver> ();
			_beaconReceiver.BeaconsUpdated += (sender, e) => BeaconsStatus = e;
			_beaconsStatus = new List<BeaconStatus> ();

			_timer = "";

		}

		public IEnumerable<BeaconStatus> BeaconsStatus 
		{ 
			get { return _beaconsStatus; }
			set {
				_beaconsStatus = value;
				OnPropertyChanged ();
			}
		}

		public Xamarin.Forms.Command StartCommand
		{
			get {
				return _startCommand = _startCommand ?? new Command (Start, () => !Started);
			}
		}

		public Xamarin.Forms.Command StopCommand
		{
			get {
				return _stopCommand = _stopCommand ?? new Command (Stop, () => Started);
			}
		}

		public string Timer
		{
			get { return _timer; }
			set {
				_timer = value;
				OnPropertyChanged ();
			}
		}

		public bool Started
		{
			get { return _started; }
			set {
				_started = value;
				StartCommand.ChangeCanExecute ();
				StopCommand.ChangeCanExecute ();
				OnPropertyChanged ();
			}
		}

		private void Start()
		{
			Started = true;
			if (_timerCancel != null)
				_timerCancel.Cancel ();
			_timerCancel = new CancellationTokenSource ();
			Task.Factory.StartNew (StartTimer);
		}

		private void Stop()
		{
			Started = false;
			if (_timerCancel != null)
				_timerCancel.Cancel ();
			_timerCancel = null;
		}

		private async Task StartTimer()
		{
			try
			{
				var cancel = _timerCancel;
				var st = Stopwatch.StartNew();
				Timer = "00:00";
				while (!cancel.IsCancellationRequested) {
					await Task.Delay (1000, cancel.Token);
					Timer = string.Format("{0:00}:{1:00}", st.ElapsedMilliseconds/60000, (st.ElapsedMilliseconds/1000)%60000);
				}
			}
			catch {
			}
		}

		private void OnPropertyChanged([CallerMemberName] string name = null)
		{
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(name));
			}
		}
	}
}

