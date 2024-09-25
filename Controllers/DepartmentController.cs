using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Repository.Repositories;
using Company.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MVCProjects.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var dept = departmentService.GetAll();
            return View(dept);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    departmentService.Add(department);
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Department Error", "Validation Errors");
                return View(department);
            }
        catch(Exception ex)
            {
                ModelState.AddModelError("Department Error", ex.Message);
                return View(department);
            }

        }
        public IActionResult Details(int id)
        {
            var dept = departmentService.GetById(id);
            if (dept is null)
            {
                return NotFound();
            }
            return View(dept);
        
            return View();
        }
    }
}
