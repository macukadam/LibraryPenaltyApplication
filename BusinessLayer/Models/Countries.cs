using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class Countries
    {
		[Key]
		public int ContryId { get; set; }
		public string Country { get; set; }
		public string Money { get; set; }
		public double Penalty { get; set; }
		public string Weekends { get; set; }
	}
}
