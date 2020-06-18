using System.Threading.Tasks;
using shared.Models;
using frontend.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace frontend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddTransient<ICRUDService<ArticleCategoryListItem, ArticleCategoryItem>, ArticleCategoriesService>();
            builder.Services.AddTransient<ICRUDService<ArticleListItem, ArticleItem>, ArticlesService>();
            builder.Services.AddTransient<HttpClient>();
            await builder.Build().RunAsync();
        }
    }
}
