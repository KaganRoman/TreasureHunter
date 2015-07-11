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
					return Color.FromHex ("#009933");
				if (status.ProximityValue == "2")
					return Color.FromHex ("#FF6600");
				if (status.ProximityValue == "3")
					return Color.FromHex ("#FFCC00");
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

