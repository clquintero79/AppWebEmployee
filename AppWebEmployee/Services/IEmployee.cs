using AppWebEmployee.Models;
using AppWebEmployee.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppWebEmployee.Services
{
    public interface IEmployee
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee Add(Employee employee);
        Employee GetEmployee(int id);
       
     
        void Update(Employee employeeChanges);
        void Delete(int Id);
        SummaryEmployee TotEmpDept(string dep);
    }
}
