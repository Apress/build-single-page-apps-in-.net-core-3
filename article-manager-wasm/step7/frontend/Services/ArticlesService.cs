using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using shared.Models;

namespace frontend.Services
{
    public class ArticlesService
        : ICRUDService<ArticleListItem, ArticleItem>
    {
        private string baseUrl = "http://localhost:5002";

        private readonly HttpClient _httpClient = null;

        public ArticlesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<ArticleListItem[]> GetList()
        {
            return _httpClient
                .GetFromJsonAsync<ArticleListItem[]>
                    ($"{baseUrl}/api/articles");
        }

        public Task<ArticleItem> GetNew()
        {
            return _httpClient
                .GetFromJsonAsync<ArticleItem>
                    ($"{baseUrl}/api/articles/new");
        }
        public Task<ArticleItem> Get(int id)
        {
            return _httpClient
                .GetFromJsonAsync<ArticleItem>
                    ($"{baseUrl}/api/articles/{id}");
        }

        public Task Create(ArticleItem item)
        {
            return _httpClient
                .PostAsJsonAsync<ArticleItem>
                    ($"{baseUrl}/api/articles", item);
        }

        public Task Update(ArticleItem item)
        {
            return _httpClient
                .PutAsJsonAsync
                    ($"{baseUrl}/api/articles/{item.Id}", item);
        }

        public Task Delete(int id)
        {
            return _httpClient
                .DeleteAsync($"{baseUrl}/api/articles/{id}");
        }
    }
}