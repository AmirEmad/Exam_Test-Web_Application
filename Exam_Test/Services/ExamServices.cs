using Exam_Test.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using NuGet.Protocol;

namespace Exam_Test.Services
{
    public class ExamServices : IExamServices<ExamModel>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient httpClient;
        private  HttpResponseMessage httpResponseMessage;
        public ExamServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            httpClient = _httpClientFactory.CreateClient("NewExam");
        }
        public async Task<ExamModel> Add(ExamModel entity)
        {
            var entityjson = new StringContent(entity.ToJson(), Encoding.UTF8, Application.Json);

            httpResponseMessage = await httpClient.PostAsync("ExamTitles", entityjson);

            httpResponseMessage.EnsureSuccessStatusCode();
            return entity;
        }

        public async void DeleteAsync(ExamModel entity)
        {
            var entityjson = new StringContent(entity.ToJson(), Encoding.UTF8, Application.Json);
            httpResponseMessage = await httpClient.DeleteAsync($"ExamTitles/{entity.Id}");
            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<ExamModel>> GetAllAsync()
        {
            httpResponseMessage = httpClient.GetAsync("ExamTitles").Result;
            
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = httpResponseMessage.Content.ReadAsAsync<IList<ExamModel>>();
                var result = contentStream.Result ;
                return result;
            }
            return null;
        }

        public ExamModel GetByIdAsync(int id)
        {
            httpResponseMessage = httpClient.GetAsync($"ExamTitles/{id}").Result;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = httpResponseMessage.Content.ReadAsAsync<ExamModel>();

                var exam = contentStream.Result;
                return exam;
            }
            return null;
        }

        public async Task<ExamModel> Update(ExamModel entity)
        {
            var entityjson = new StringContent(entity.ToJson(), Encoding.UTF8, Application.Json);
            using var httpResponseMessage = await httpClient.PutAsync($"ExamTitles/{entity.Id}", entityjson);
            httpResponseMessage.EnsureSuccessStatusCode();
            return entity;
        }
    }
}
