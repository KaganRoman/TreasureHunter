using System;
using BeaconTest.iOS;
using AudioToolbox;
using Foundation;
using AVFoundation;

[assembly: Xamarin.Forms.Dependency (typeof (AudioService))]
namespace BeaconTest.iOS
{
	public class AudioService : IAudio
	{
		public AudioService ()
		{
		}

		#region IAudio implementation

		public void PlayMp3File (string fileName)
		{
			var url = NSUrl.FromFilename (fileName);
			var systemSound = new SystemSound (url);
			systemSound.PlaySystemSound ();
		}

		public void PlayWavFile (string fileName)
		{
		}


		public void Vibrate ()
		{
			SystemSound.Vibrate.PlaySystemSound();
		}
		#endregion
	}
}

