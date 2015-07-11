using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Linq;

namespace BeaconTest
{
	public class MainPageVM : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
			
		private List<BeaconStatus> _beaconsStatus;

		private string _timer;
		private CancellationTokenSource _timerCancel;

		private Xamarin.Forms.Command _startCommand;
		private Xamarin.Forms.Command _stopCommand;
		private bool _started;

		private IAudio _sound;

		public MainPageVM ()
		{
			_sound = DependencyService.Get<IAudio> ();

			var beaconReceiver = DependencyService.Get<IBeaconReceiver> ();
			beaconReceiver.BeaconsUpdated += (sender, e) => BeaconsStatus = UpdateStatus(e);

			_beaconsStatus = new List<BeaconStatus> ();
			for (int i = 0; i < 10; ++i)
				_beaconsStatus.Add (new BeaconStatus { Name = string.Format("T{0}", i)});

			_timer = "";

		}

		public List<BeaconStatus> BeaconsStatus 
		{ 
			get { return _beaconsStatus; }
			set {
				_beaconsStatus = value;
				OnPropertyChanged ();
				OnPropertyChanged ("Found");
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
				return _stopCommand = _stopCommand ?? new Command (() => Stop(), () => Started);
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

		public string Found
		{
			get { return string.Format("Found {0} chests", _beaconsStatus.Count(b => b.Visited)); }
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

		private List<BeaconStatus> UpdateStatus(IEnumerable<BeaconStatus> updates)
		{
			var added = false;
			var l = new List<BeaconStatus> (_beaconsStatus);
			foreach (var update in updates) {
				var old = l.First (b => b.Name == update.Name);
				if (!old.Visited && update.ProximityValue == "1") 
					added = true;
				update.Visited = old.Visited || update.ProximityValue == "1";
				var index = l.IndexOf (old);
				l.Remove (old);
				l.Insert (index, update);
			}
			if (added) {
				_sound.Vibrate ();
				if (l.All (b => b.Visited)) {
					_sound.PlayMp3File ("Sounds/tada.aif");
					if (Started)
						Stop (false);
				} else {
					_sound.PlayMp3File ("Sounds/coin.wav");	
				}
			} 
			return l;
		}

		private void ResetBeacons()
		{
			var l = new List<BeaconStatus>(_beaconsStatus);
			foreach (var b in l)
				b.Visited = false;
			BeaconsStatus = l;
		}

		private void Start()
		{
			_sound.PlayMp3File ("Sounds/motorcycle.aif");	
				
			Started = true;
			if (_timerCancel != null)
				_timerCancel.Cancel ();
			_timerCancel = new CancellationTokenSource ();
			ResetBeacons ();
			Task.Factory.StartNew (StartTimer);
		}

		private void Stop(bool playSound = true)
		{
			if(playSound)
				_sound.PlayMp3File ("Sounds/Toilet-Flush.aif");	
				
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
					Timer = string.Format("{0:00}:{1:00}", st.ElapsedMilliseconds/60000, (st.ElapsedMilliseconds/1000)%60);
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

