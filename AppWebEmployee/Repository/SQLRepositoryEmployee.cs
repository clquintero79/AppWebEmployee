using AppWebEmployee.Models;
using AppWebEmployee.Services;
using AppWebEmployee.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppWebEmployee.Repository
{
    public class SQLRepositoryEmployee:IEmployee
    {
        private readonly AppEmployeeContext context;

        public SQLRepositoryEmployee(AppEmployeeContext context)
        {
            this.context = context;
        }

        public Employee Add(Employee employee)
        {
            context.Employee.Add(employee);
            context.SaveChanges();
            return employee;
        }



        public void Delete(int Id)
        {
            Employee employee = context.Employee.Find(Id);
            if (employee != null)
            {
                context.Employee.Remove(employee);
                context.SaveChanges();
            }
           
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return context.Employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return context.Employee;
        }

        public Employee GetEmployee(int Id)
        {
            return context.Employee.Find(Id);
        }

        public SummaryEmployee TotEmpDept(string dep)
        {
            var summaryEmployee = new SummaryEmployee()
            {
                TotalEmployees = context.Employee.Count(),
                EmployeesDepartment = context.Employee.Where(x => x.Department.ToString() == dep).Count()
            };
            return summaryEmployee;
        }

        public void Update(Employee employeeChanges)
        {
            var employee = context.Employee.Attach(employeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            
        }

       
    }
}
