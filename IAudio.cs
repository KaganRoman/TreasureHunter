using System;

namespace BeaconTest
{
	public interface IAudio
	{
		void PlayMp3File(string fileName);
		void PlayWavFile(string fileName);

		void Vibrate();
	}
}

