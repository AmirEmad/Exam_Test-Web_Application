using Exam_Test.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using NuGet.Protocol;
using System.IO;
using Exam_Test.Services;

namespace Exam_Test.Controllers
{
    public class ExamController : Controller
    {
        private readonly IExamServices<ExamModel> _examServices;

        public ExamController(IExamServices<ExamModel> examServices)
        {
            _examServices = examServices;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _examServices.GetAllAsync());
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExamModel examModel)
        {
            _examServices.Add(examModel);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var exam = _examServices.GetByIdAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            return View(exam);
        }

        [HttpPost]
        public async Task<IActionResult> ConfiemEdit(ExamModel examModel)
        {
            _examServices.Update(examModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var exam = _examServices.GetByIdAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            return View(exam);
        }

        [HttpPost]
        public async Task<IActionResult> ConfiemDelete(ExamModel examModel)
        {
            _examServices.DeleteAsync(examModel);
            return RedirectToAction("Index");
        }


    }
}
