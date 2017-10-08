using BookReturnPenaltyApp.CustomDataAnnotations;
using BusinessLayer;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookReturnPenaltyApp.ViewModels
{
    public class HolidayFormViewModel
    {
		[Required]
		[Display(Name = "Book Check-In Date")]
		public DateTime BookCheckInDate { get; set; }
		[Required]
		//[GreaterThan("BookCheckInDate")]
		[Display(Name = "Book Check-Out Date")]
		[IfDateIsBigger]
		public DateTime BookCheckOut { get; set; }
		[Required]
		public List<Countries> Country { get; set; }

		public int CountryId { get; set; }

	}
}
