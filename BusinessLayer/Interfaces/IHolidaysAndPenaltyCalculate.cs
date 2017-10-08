using BusinessLayer.BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
	public interface IHolidaysAndPenaltyCalculate
	{
		double CalculateDays(DateTime BookCheckInDate, DateTime BookChectOutDate, HolidayInfo HolidayInfo);
	}
}
