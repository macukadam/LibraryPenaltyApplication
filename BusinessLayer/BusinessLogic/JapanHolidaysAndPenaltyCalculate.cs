using BusinessLayer.BusinessModels;
using BusinessLayer.Interfaces;
using System;
using System.Linq;

namespace BusinessLayer.BusinessLogic
{
    public class JapanHolidaysAndPenaltyCalculate : IHolidaysAndPenaltyCalculate
	{
		//Tatil Gunu Hesaplamasi
		public double CalculateDays(DateTime BookCheckInDate, DateTime BookChectOutDate, HolidayInfo HolidayInfo)
		{
			double totalHolidayCount = 0;
			for (DateTime k = BookCheckInDate; k < BookChectOutDate;)
			{
				var weekends = HolidayInfo.weekEnds.Split('/').Select(Int32.Parse).ToList();

				if (weekends.Contains((int)k.DayOfWeek))
				{
					totalHolidayCount += 1;
					//eger haftasonu ise devam et
					k = k.AddDays(1);
					continue;
				}

				if (HolidayInfo.Days.Contains(k.DayOfYear))
				{
					var index = HolidayInfo.Days.IndexOf(k.DayOfYear);
					totalHolidayCount += HolidayInfo.HolidayDayLength.ElementAt(index) * 1;
				}
				k = k.AddDays(1);
			}

			return totalHolidayCount;
		}


	}
}
