using Company.Data.Contexts;
using Company.Data.Models;
using Company.Service.Dto;
using Company.Service.Interfaces;
using Company.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVCProjects.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IDepartmentService departmentService;

        public EmployeeController(IEmployeeService employeeService,IDepartmentService departmentService)
        {
            this.employeeService = employeeService;
            this.departmentService = departmentService;
        }
        [HttpGet]
        public IActionResult Index(string searchInp)
        {
            if(searchInp is null)
            {
                var emp = employeeService.GetAll();
                //ViewBag.Message = "This Message From ViewBag";
                //ViewData["TextMessage"] = "This Message From ViewData";
                //TempData["TextMessage2"]= "This Message From TempData";
                return View(emp);
            }
            else
            {
                var emp = employeeService.GetEmployeeByName(searchInp);
                return View(emp);
            }
          
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Department = departmentService.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeDto employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employeeService.Add(employee);
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Department Error", "Validation Errors");
                return View(employee);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Department Error", ex.Message);
                return View(employee);
            }

        }
    }
}
