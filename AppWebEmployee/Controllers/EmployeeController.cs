using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppWebEmployee.Models;
using AppWebEmployee.Services;
using AppWebEmployee.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AppWebEmployee.Controllers
{
    public class EmployeeController : Controller
    {
        #region Atributos

        IEmployee EmployeeRepository { get; set; }

        #endregion

        #region Constructores

        public EmployeeController(IEmployee employeeRepository)
        {
            EmployeeRepository = employeeRepository;
        }
        #endregion
        #region Acciones

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Mensaje"] = "Esta es la lista de los empleados de la empresa";
            var model = EmployeeRepository.GetAllEmployees();
            return View(model);
            //return "Hola soy el controlador de Employee";
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                Employee newEmployee = EmployeeRepository.Add(employee);
                //return RedirectToAction("Index");
                //return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View();
        }

        public IActionResult Detail(int id)
        {
            var model =EmployeeRepository.GetEmployee(id);
            return View(model);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {

            var model = EmployeeRepository.GetEmployee(id);
            return View(model);
        }

        [HttpPost]

        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                EmployeeRepository.Update(employee);
                return RedirectToAction("Index");
                //return RedirectToAction("details", new { id = newEmployee.Id });
            }

            

            return View();
        }
        public IActionResult Delete(int id)
        {
            EmployeeRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Total(string dept)
        {
            var model = EmployeeRepository.TotEmpDept(dept);
            ViewData["Department"] = dept;
            return View("../ViewModels/SummaryEmployee", model);
        }


        #endregion

    }
}