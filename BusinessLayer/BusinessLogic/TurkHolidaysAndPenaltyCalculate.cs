using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.BusinessModels;
using BusinessLayer.Interfaces;
using BusinessLayer.Helpers;

namespace BusinessLayer.BusinessLogic
{
	public class TurkHolidaysAndPenaltyCalculate : IHolidaysAndPenaltyCalculate
	{
		private double totalHolidayCount = 0;
		private enum referanceYear { refYear = 2017 }
		private enum delay { religiousHoliday = 10 }

		//Dini bayramlarda girilen tarihe göre yaşanabilecek kaymalar için özel hesap yapılacaktır
		//(Tatillerin referans yılı 2017 olarak seçildi. Dini bayramlar için 2017 ye göre +10 ya da -10 gün kayma olacak!!)

		public double CalculateDays(DateTime BookCheckInDate, DateTime BookChectOutDate, HolidayInfo HolidayInfo)
		{
			List<HolidayInfo> dates = new List<HolidayInfo>();

			for (int i = BookCheckInDate.Year; i <= BookChectOutDate.Year; i++)
			{
				var updatedHolidays = HolidayInfo;
				
				foreach (TurkBayramTatilleri tatil in Enum.GetValues(typeof(TurkBayramTatilleri)))
				{
					var dayIndex = (int)tatil;

					
					var deflection = updatedHolidays.Days.ElementAt(dayIndex) + ((int)referanceYear.refYear - i) * (int)delay.religiousHoliday;

					if (deflection > DaysInAYear.DayCount(i))
					{
						deflection = deflection - DaysInAYear.DayCount(i);
					}
					else if (deflection < 0)
					{
						deflection = deflection + DaysInAYear.DayCount(i);
					}
					updatedHolidays.Days[dayIndex]=deflection;
					
				}
				dates.Add(updatedHolidays);
			}



			int beginningYear = BookCheckInDate.Year;
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

				var dayRow = dates.ElementAt(k.Year - beginningYear);

				if (dayRow.Days.Contains(k.DayOfYear))
				{
					var index = dayRow.Days.IndexOf(k.DayOfYear);
					totalHolidayCount += dayRow.HolidayDayLength.ElementAt(index) * 1;
				}
				k = k.AddDays(1);
			}

			return totalHolidayCount;
		}
	}
}