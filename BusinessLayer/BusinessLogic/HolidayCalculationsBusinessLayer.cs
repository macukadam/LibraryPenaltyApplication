using System;
using BusinessLayer.Models;
using BusinessLayer.Data;
using System.Linq;
using BusinessLayer.BusinessModels;
using BusinessLayer.BusinessLogic;
using BusinessLayer.Interfaces;

namespace BusinessLayer
{
	public enum CountriesEnum { Turkey = 1, Japan }
	public enum TurkBayramTatilleri
	{
		RamazanBayramıArifesi = 5,
		RamazanBayramıgün1 = 6,
		RamazanBayramıgün2 = 7,
		RamazanBayramıgün3 = 8,
		KurbanBayramıArifesi = 11,
		KurbanBayramıgün1 = 12,
		KurbanBayramıgün2 = 13,
		KurbanBayramıgün3 = 14,
		KurbanBayramıgün4 = 15
	}
	public enum ReturnLimitDays { Days=10 }

	public class HolidayCalculationsBusinessLayer<TCountry> : IHolidayFinder where TCountry : IHolidaysAndPenaltyCalculate
	{
		protected ApplicationDbContext _context;
		IHolidaysAndPenaltyCalculate _penalty;
		public HolidayCalculationsBusinessLayer(ApplicationDbContext context, TCountry penalty)
		{
			_context = context;
			_penalty = penalty;
		}
		//Holidayinfo modelini Holidays tablosu ve Country bilgileriyle doldurdum.
		public HolidayInfo Holidays(int CountryId)
		{
			var holidays = _context.Holidays.Where(h => h.Country.ContryId == CountryId).Select(h => h.DayOfYearFor2017).ToList();
			var dayLength = _context.Holidays.Where(h => h.Country.ContryId == CountryId).Select(h => h.HolidayLength).ToList();


			var weekEnds = _context.Countries.Where(c => c.ContryId == CountryId).Select(c => c.Weekends).First();
			var currenct = _context.Countries.Where(c => c.ContryId == CountryId).Select(c => c.Money).First();
			var penaltyAmount = _context.Countries.Where(c => c.ContryId == CountryId).Select(c => c.Penalty).First();

			var HolidayInfo = new HolidayInfo
			{
				Days = holidays,
				HolidayDayLength = dayLength,
				weekEnds = weekEnds,
				currenct = currenct,
				penaltyAmount = penaltyAmount
			};

			return HolidayInfo;
		}

		//Ceza hesaplamasi
		public double PenaltyCalculation(DateTime BookCheckInDate, DateTime BookCheckOutDate, int CountryId)
		{
			var holidayInfo = Holidays(CountryId);
			var totalHolidayCount = _penalty.CalculateDays(BookCheckInDate, BookCheckOutDate, holidayInfo);
			var bookReturnDelay = (BookCheckOutDate - BookCheckInDate).Days - totalHolidayCount;

			if (bookReturnDelay <= (int)ReturnLimitDays.Days)
			{
				return 0;
			}
			else
			{
				var penalty = (bookReturnDelay - (int)ReturnLimitDays.Days) * holidayInfo.penaltyAmount;
				return penalty < 0 ? 0 : penalty;
			}

		}
	}
}
