using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Repository.Repositories;
using Company.Service.Dto;
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
        public IActionResult Create(DepartmentDto department)
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
        public IActionResult Details(int id,string viewname="Details")
        {
            var dept = departmentService.GetById(id);
            if (dept is null)
            {
                return NotFound();
            }
            return View(viewname, dept);
        
            
        }
        [HttpGet]
        //public IActionResult Update(int ?id)
        //{
        //    return Details(id,"Update");
        //}
        //[HttpPost]
        //public IActionResult Update(int?id,Department department)
        //{
        //   if(department.ID!=id.Value)
            
        //        return RedirectToAction("NotFoundPage",null,"Home");
              
            
        //    departmentService.Update(department);
        //    return RedirectToAction(nameof(Index));
        //}
        public IActionResult Delete(int id)
        {
            var dept = departmentService.GetById(id);
            if (dept is null)
                return RedirectToAction("NotFoundPage", null, "Home");
            //dept.IsDeleted = true;
            //departmentService.Update(dept);
            //departmentService.Delete(dept);
            return RedirectToAction(nameof(Index));
          
        }
    }
}
