using Exam_Test.Models;
using Exam_Test.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Exam_Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IExamServices<ExamModel> _examServices;

        public HomeController(ILogger<HomeController> logger , IExamServices<ExamModel> examServices)
        {
            _logger = logger;
            _examServices = examServices;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _examServices.GetAllAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}