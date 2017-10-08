using BusinessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Data
{
    public class ApplicationDbContext:DbContext
    {
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Countries> Countries { get; set; }
		public DbSet<Holidays> Holidays { get; set; }
	}
}
