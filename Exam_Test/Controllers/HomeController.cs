using Exam_Test.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Exam_Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger , IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            IEnumerable<ExamModel> exams = null;

            var httpClient = _httpClientFactory.CreateClient("NewExam");
            var httpResponseMessage = httpClient.GetAsync("ExamTitles");

            if (httpResponseMessage.Result.IsSuccessStatusCode)
            {
                using var contentStream = httpResponseMessage.Result.Content.ReadAsAsync<IList<ExamModel>>();

                exams = contentStream.Result;
            }

            return View(exams);
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