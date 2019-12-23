using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Tutorial.Web.Model;
using Tutorial.Web.Services;
using Tutorial.Web.ViewModels;

namespace Tutorial.Web.Controllers
{
    public class HomeController:Controller
    {
        private readonly IRepository<Student> _repository;

        public HomeController(IRepository<Student> repository)
        {
            this._repository = repository;
        }
        public IActionResult Index()
        {
            //this.HttpContext
            //return "hello from HomeController";
            //return Content("hello from HelloController");

            //var st = new Student
            //{
            //    Id = 1,
            //    FirstName = "Qilong",
            //    LastName = "Wu"
            //};
            //return new ObjectResult(st);
            var list = _repository.GetAll();
            var studentList = list.Select(x => new StudentViewModel
            {
                Id = x.Id,
                Name = $"{x.FirstName} {x.LastName}",
                Age = DateTime.Now.Subtract(x.BirthDate).Days / 365
            }) ;
            var homeIndexViewModel = new HomeIndexViewModel
            {
                Students = studentList,
            };
            return View(homeIndexViewModel);
        }

        public IActionResult Detail(int id)
        {
            var student = _repository.GetById(id);
            if(student == null)
            {
                //return View("NotFound");
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentCreateViewModel student)
        {
            if (ModelState.IsValid)
            {
                var newStudent = new Student
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    BirthDate = student.BirthDate,
                    Gender = student.Gender
                };
                var newModel = _repository.Add(newStudent);
                //return View("Detail", newModel);
                return RedirectToAction(nameof(Detail), new { id = newModel.Id });

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Model level Error!");
                return View();
            }
        }

    }
}
