using System.Threading.Tasks;
using shared.Models;
using System.Net.Http.Json;
using System.Net.Http;

namespace frontend.Services
{
    public class ArticleCategoriesService
        : ICRUDService<ArticleCategoryListItem, ArticleCategoryItem>
    {
        private string baseUrl = "http://localhost:5002";
        private readonly HttpClient _httpClient = null;

        public ArticleCategoriesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<ArticleCategoryListItem[]> GetList()
        {
            return _httpClient
                .GetFromJsonAsync<ArticleCategoryListItem[]>
                    ($"{baseUrl}/api/articlecategories");
        }

        public Task<ArticleCategoryItem> GetNew()
        {
            return Task.FromResult(
                new ArticleCategoryItem()
            );
        }
        public Task<ArticleCategoryItem> Get(int id)
        {
            return _httpClient
                .GetFromJsonAsync<ArticleCategoryItem>
                    ($"{baseUrl}/api/articlecategories/{id}");
        }

        public Task Create(ArticleCategoryItem item)
        {
            return _httpClient
                .PostAsJsonAsync<ArticleCategoryItem>
                    ($"{baseUrl}/api/articlecategories", item);
        }

        public Task Update(ArticleCategoryItem item)
        {
            return _httpClient
                .PutAsJsonAsync
                    ($"{baseUrl}/api/articlecategories/{item.Id}", item);
        }

        public Task Delete(int id)
        {
            return _httpClient
                .DeleteAsync($"{baseUrl}/api/articlecategories/{id}");
        }
    }
}