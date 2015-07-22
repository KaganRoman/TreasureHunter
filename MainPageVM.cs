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

		private string _userId;
		private string _name;

		private string _positionStatus;

		private IServer _server;

		public MainPageVM ()
		{
			_sound = DependencyService.Get<IAudio> ();
			_server = DependencyService.Get<IServer> ();

			var beaconReceiver = DependencyService.Get<IBeaconReceiver> ();
			_userId = beaconReceiver.UserId;

			beaconReceiver.BeaconsUpdated += (sender, e) => BeaconsStatus = UpdateStatus(e);
				
			_beaconsStatus = new List<BeaconStatus> ();
			for (int i = 0; i < 10; ++i)
				_beaconsStatus.Add (new BeaconStatus { Name = string.Format("T{0}", i)});

			_timer = "00:00";
			_positionStatus = "";
			_name = "";

			_server.PositionsUpdated += UpdateUsersFromServer;
			UpdateServer ();
		}

		public List<BeaconStatus> BeaconsStatus 
		{ 
			get { return _beaconsStatus; }
			set {
				_beaconsStatus = value;
				OnPropertyChanged ();
				OnPropertyChanged ("Found");
				UpdateServer ();
			}
		}

		public Xamarin.Forms.Command StartCommand
		{
			get {
				return _startCommand = _startCommand ?? new Command (Start, () => !string.IsNullOrEmpty(Name) && !Started);
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

		public int Found
		{
			get { return _beaconsStatus.Count(b => b.Visited); }
		}

		public string Status
		{
			get { return _positionStatus; }
			set {
				_positionStatus = value;
				OnPropertyChanged ();
			}
		}

		public string Name
		{
			get { return _name; }
			set {
				_name = value;
				StartCommand.ChangeCanExecute ();
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

		private List<BeaconStatus> UpdateStatus(IEnumerable<BeaconStatus> updates)
		{
			if (!Started)
				return _beaconsStatus;
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
			Task.Factory.StartNew (async () => await StartTimer());
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

					if((st.ElapsedMilliseconds/1000)%30 == 0)
						_sound.PlayMp3File ("Sounds/shame.wav");	
					if((st.ElapsedMilliseconds/1000)%5 == 0)
						UpdateServer();
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

		class ServerStatus
		{
			public string Name { get; set; }
			public string Status { get; set; }
			public string UserId { get; set; }
			public string Timer { get; set; }
			public List<BeaconStatus> Beacons { get; set; }
		}

		private void UpdateServer()
		{
			var message = new ServerStatus 
			{ 
				UserId = _userId, 
				Status = _started ? "Running" : "Waiting", 
				Beacons = _beaconsStatus ,
				Timer = Timer,
				Name = Name
			};

			_server.UpdateServer (message);
		}

		private void UpdateUsersFromServer(object sender, Dictionary<string, int> users)
		{
			try
			{
				var usersCount = users.Count - 1;
				var userPos = -1;
				if(users.ContainsKey(_userId))
					userPos = users[_userId];

				Device.BeginInvokeOnMainThread(() =>
					{
						Status = string.Format("{0} pirates around, your position is {1}", usersCount, userPos);
					});
			}
			catch(Exception e) {
			}
		}
	}
}

