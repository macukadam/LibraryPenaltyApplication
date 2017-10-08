using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Helpers
{
    static class DaysInAYear
    {
		//integer olarak verilen bir yildaki gun sayisini hesaplamak icin yardimci method
		public static int DayCount(int value)
		{
			int Year = value;

			int RetVal = (new DateTime(Year + 1, 1, 1) - new
			DateTime(Year, 1, 1)).Days;
			return RetVal;
		}
	}
}
