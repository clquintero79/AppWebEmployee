using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AppWebEmployee.Models;
using AppWebEmployee.Services;
using AppWebEmployee.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AppWebEmployee.Controllers
{
    public class EmployeeController : Controller
    {
        #region Atributos

        IEmployee EmployeeRepository { get; set; }
        private readonly IHostingEnvironment hostingEnvironment;

        #endregion

        #region Constructores

        public EmployeeController(IEmployee employeeRepository, IHostingEnvironment hosting)
        {
            EmployeeRepository = employeeRepository;
            hostingEnvironment = hosting;
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
        public ViewResult Create()
        {
            return View("CreateViewModel");
        }



        [HttpPost]

        public IActionResult Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                // Si es diferente de nulo ha seleccionado una iamgen a subir
                if (model.Photo != null)
                {
                    // Las iamgenes se van a s subir en la carpeta images de wwroot
                    //Con el proposito que lo reconozca vamos a inyectar el servicio
                    // HostingEnvironment service de ASP.NET Core
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    // Para asegurarse que el nombre será único le vamos asignar un
                    // GUID _ y el nombre dle archivo
                    uniqueFileName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}_{model.Photo.FileName}";
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Utilizamos CopyTo()de la interfaz IFormFile para
                    // copiar el archivo a wwwroot/images 
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    // Guardamos el nombre del archivo en la propiedad PhotoPath 
                    PhotoPath = uniqueFileName
                };

                EmployeeRepository.Add(newEmployee);
                return RedirectToAction("Detail", new { id = newEmployee.Id });

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