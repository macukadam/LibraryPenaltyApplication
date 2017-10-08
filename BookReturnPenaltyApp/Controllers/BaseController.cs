using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Data;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookReturnPenaltyApp.Controllers
{
    public class BaseController : Controller
    {
		protected ApplicationDbContext _context;

		public BaseController(ApplicationDbContext context)
		{
			_context = context;
		}
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			base.OnActionExecuted(context);
		}
	}
}