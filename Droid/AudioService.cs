using System;
using BeaconTest.Droid;

[assembly: Xamarin.Forms.Dependency (typeof (AudioService))]
namespace BeaconTest.Droid
{
	public class AudioService: IAudio
	{
		public AudioService ()
		{
		}

		#region IAudio implementation

		public void PlayMp3File (string fileName)
		{
		}

		public void PlayWavFile (string fileName)
		{
		}

		public void Vibrate ()
		{
		}

		#endregion
	}
}

