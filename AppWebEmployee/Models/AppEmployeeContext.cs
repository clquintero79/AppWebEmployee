using AppWebEmployee.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppWebEmployee.Models
{
    public class AppEmployeeContext:DbContext
    {
        public AppEmployeeContext(DbContextOptions<AppEmployeeContext> options): base(options)
        {

        }
        public DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
               new Employee
               {
                   Id = 1,
                   Name = "Johan",
                   Department = Dept.Sistemas,
                   Email = "mio@gmail.com"
               }
           );
        }
    }
}
