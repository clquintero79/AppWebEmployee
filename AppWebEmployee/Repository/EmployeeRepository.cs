using AppWebEmployee.Models;
using AppWebEmployee.Services;
using AppWebEmployee.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppWebEmployee.Repository
{
    public class EmployeeRepository : IEmployee
    {

        #region Atributos
        List<Employee> employeesList;
        #endregion

        #region Constructores
         public EmployeeRepository()
        {
            employeesList = new List<Employee>()
            {
                //new Employee() { Id = 1, Name = "Mariana", Department = "Administrativo", Email = "mio@gmail.com" },
                //new Employee() { Id = 2, Name = "Sonia", Department = "Financiero", Email = "mio2@gmail.com" },
                //new Employee() { Id = 3, Name = "Samuel", Department = "Sistemas", Email = "mio3@gmail.com" },

                new Employee() { Id = 1, Name = "Eliana", Department = Dept.Administrativo, Email = "mio1@gmail.com" },
                new Employee() { Id = 2, Name = "Samuel", Department = Dept.Financiero, Email = "mio2@gmail.com" },
                new Employee() { Id = 3, Name = "Manuela", Department = Dept.Recursos_Humanos, Email = "mio3@gmail.com" },
            };

        }
        #endregion
        public Employee Add(Employee employee)
        {
            employee.Id = employeesList.Max(e => e.Id) + 1;
            employeesList.Add(employee);
            return employee;
        }

        public void Delete(int Id)
        {
            var emp = employeesList.FirstOrDefault(x => x.Id == Id);
            employeesList.Remove(emp);
           
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return employeesList;
        }

        public Employee GetEmployee(int id)
        {
            var employee = employeesList.FirstOrDefault(x => x.Id == id);
            return employee;
        }

        public void Update(Employee employeeChanges)
        {
            var emp = GetAllEmployees().FirstOrDefault(x => x.Id == employeeChanges.Id);
            emp.Name = employeeChanges.Name;
            emp.Email = employeeChanges.Email;
            emp.Department = employeeChanges.Department;

        }

        public SummaryEmployee TotEmpDept(string dep)
        {
            var summaryEmployee = new SummaryEmployee()
            {
                TotalEmployees = employeesList.Count(),
                EmployeesDepartment = employeesList.Where(x => x.Department.ToString() == dep).Count()
            };
            return summaryEmployee;
        }
    }
}
