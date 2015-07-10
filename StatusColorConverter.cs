using System;
using Xamarin.Forms;

namespace BeaconTest
{
	public class StatusColorConverter : IValueConverter
	{
		public StatusColorConverter ()
		{
		}

		#region IValueConverter implementation

		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var status = value as BeaconStatus;
			if (status != null) {
				if (status.Visited)
					return Color.Green;
				if (status.ProximityValue == "2")
					return Color.Red;
				if (status.ProximityValue == "3")
					return Color.Yellow;
				if (status.ProximityValue == "4")
					return Color.Blue;
			}
			return Color.FromHex ("#E8F0FD");
		}

		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

