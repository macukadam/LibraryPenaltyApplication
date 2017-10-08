using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Models;
using BusinessLayer.Data;
using BusinessLayer.BusinessModels;
using BusinessLayer.BusinessLogic;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc.Filters;
using BookReturnPenaltyApp.ViewModels;
using Newtonsoft.Json;

namespace BookReturnPenaltyApp.Controllers
{
	public class HomeController : BaseController
	{
		public HomeController(ApplicationDbContext context) : base(context) { }

		//5$ a tekabul edecek sekilde Turkiye icin 15TL Japonya icin 550Yen gunluk ceza miktari olarak secilmistir.

		public IActionResult Index()
		{
			var holidayInfo = new HolidayFormViewModel
			{
				BookCheckInDate = DateTime.Now,
				BookCheckOut = DateTime.Now,
				Country = _context.Countries.ToList(),
			};
			return View(holidayInfo);
		}

		[HttpPost]
		public IActionResult Index(HolidayFormViewModel viewModel)
		{		
			string result = null;
			var errors = ModelState.Values.SelectMany(v => v.Errors);
			if (!ModelState.IsValid)
			{
				var holidayInform = new HolidayFormViewModel
				{
					BookCheckInDate = DateTime.Now,
					BookCheckOut = DateTime.Now,
					Country = _context.Countries.ToList(),
				};
				return View(holidayInform);
			}

			if (viewModel.CountryId == (int)CountriesEnum.Turkey)
			{
				TurkHolidaysAndPenaltyCalculate calculate = new TurkHolidaysAndPenaltyCalculate();
				HolidayCalculationsBusinessLayer<TurkHolidaysAndPenaltyCalculate> ret = new HolidayCalculationsBusinessLayer<TurkHolidaysAndPenaltyCalculate>(_context, calculate);
				result = ret.PenaltyCalculation(viewModel.BookCheckInDate, viewModel.BookCheckOut, viewModel.CountryId).ToString();
			}
			else if (viewModel.CountryId == (int)CountriesEnum.Japan)
			{
				JapanHolidaysAndPenaltyCalculate calculate = new JapanHolidaysAndPenaltyCalculate();
				HolidayCalculationsBusinessLayer<JapanHolidaysAndPenaltyCalculate> ret = new HolidayCalculationsBusinessLayer<JapanHolidaysAndPenaltyCalculate>(_context, calculate);
				result = ret.PenaltyCalculation(viewModel.BookCheckInDate, viewModel.BookCheckOut, viewModel.CountryId).ToString();
			}

			
			var holidayInfo = new HolidayFormViewModel
			{
				BookCheckInDate = DateTime.Now,
				BookCheckOut = DateTime.Now,
				Country = _context.Countries.ToList(),
			};
			string currency = _context.Countries.First(c => c.ContryId == viewModel.CountryId).Money;
			ViewBag.staticCurrency = result + " " + currency;

			return View(holidayInfo);
		}

	}
}
