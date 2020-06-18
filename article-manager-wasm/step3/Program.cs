using System.Threading.Tasks;
using article_manager_wasm.Models;
using article_manager_wasm.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace article_manager_wasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddTransient<ICRUDService<ArticleCategoryListItem, ArticleCategoryItem>, ArticleCategoriesService>();
            builder.Services.AddTransient<ICRUDService<ArticleListItem, ArticleItem>, ArticlesService>();
            await builder.Build().RunAsync();
        }
    }
}
