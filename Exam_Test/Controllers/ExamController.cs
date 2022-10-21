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

namespace Exam_Test.Controllers
{
    public class ExamController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        IEnumerable<ExamModel> exams = null;

        public ExamController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            var httpClient = _httpClientFactory.CreateClient("NewExam");
            var httpResponseMessage =  httpClient.GetAsync("ExamTitles");

            if (httpResponseMessage.Result.IsSuccessStatusCode)
            {
                using var contentStream =  httpResponseMessage.Result.Content.ReadAsAsync<IList<ExamModel>>();

                exams = contentStream.Result;
            }

            return View(exams);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExamModel examModel)
        {
            var httpClient = _httpClientFactory.CreateClient("NewExam");

            var examjson = new StringContent(examModel.ToJson(),Encoding.UTF8,Application.Json);

            using var httpResponseMessage = await httpClient.PostAsync("ExamTitles", examjson);
           
            httpResponseMessage.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public  IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ExamModel category = null;
            var httpClient = _httpClientFactory.CreateClient("NewExam");
            var httpResponseMessage = httpClient.GetAsync($"ExamTitles/{id}");

            if (httpResponseMessage.Result.IsSuccessStatusCode)
            {
                using var contentStream = httpResponseMessage.Result.Content.ReadAsAsync<ExamModel>();

                category = contentStream.Result;
            }
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPut]
        public async Task<IActionResult> ConfiemEdit(ExamModel examModel)
        {
            
            var httpClient = _httpClientFactory.CreateClient("NewExam");
            var examjson = new StringContent(examModel.ToJson(), Encoding.UTF8, Application.Json);
            
            using var httpResponseMessage = await httpClient.PutAsync($"ExamTitles/{examModel.Id}", examjson);

            httpResponseMessage.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        //[HttpDelete]
        //public async Task<IActionResult> Delete(ExamModel examModel)
        //{
        //    var httpClient = _httpClientFactory.CreateClient("NewExam");

        //    var examjson = new StringContent(examModel.ToJson(),Encoding.UTF8,Application.Json);

        //    using var httpResponseMessage = await httpClient.DeleteAsync("ExamTitles", examjson);

        //    httpResponseMessage.EnsureSuccessStatusCode();
        //    return RedirectToAction("Index");
        //}
    }
}
