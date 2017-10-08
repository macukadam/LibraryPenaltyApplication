using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class Holidays
    {
		[Key]
		public int HolidayId { get; set; }
		public string HolidayName { get; set; }
		public Countries Country  { get; set; }
		public int DayOfYearFor2017 { get; set; }
		public float HolidayLength { get; set; }
	}
}
