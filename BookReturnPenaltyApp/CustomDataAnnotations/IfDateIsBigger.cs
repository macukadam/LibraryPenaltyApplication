using BookReturnPenaltyApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookReturnPenaltyApp.CustomDataAnnotations
{
	public class IfDateIsBigger : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			
			var dates = (HolidayFormViewModel)validationContext.ObjectInstance;
			DateTime bci = dates.BookCheckInDate;
			DateTime bco = dates.BookCheckOut;
			if (bci > bco)
			{
				return new ValidationResult(ErrorMessage ?? "Make sure your check-Out date is bigger than check-In date");
			}
			else
			{

				return ValidationResult.Success;
			}
		}
	}
}
