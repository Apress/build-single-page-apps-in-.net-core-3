using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using article_manager.Models;

namespace article_manager.Services
{
    public class ArticlesService
        : ICRUDService<ArticleListItem, ArticleItem>
    {
        static InputSelectItem[] categories = new InputSelectItem[] {
            new InputSelectItem() { Value = "1", Label = "Category 1" },
            new InputSelectItem() { Value = "2", Label = "Category 2" },
            new InputSelectItem() { Value = "3", Label = "Category 3" }
        };

        static List<ArticleItem> articles = new List<ArticleItem>{
            new ArticleItem() { Id = 1, Title = "Title 1", CategoryId = "1", Content = "Content 1" },
            new ArticleItem() { Id = 2, Title = "Title 2", CategoryId = "2", Content = "Content 2" },
            new ArticleItem() { Id = 3, Title = "Title 3", CategoryId = "3", Content = "Content 3" },
        };

        public Task<ArticleListItem[]> GetList()
        {
            return Task.FromResult(
                articles.Select(x => new ArticleListItem()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Category = categories
                        .Where(y => y.Value == x.CategoryId)
                        .Select(y => y.Label)
                        .SingleOrDefault()
                }).ToArray()
            );
        }

        public Task<ArticleItem> GetNew()
        {
            var article = new ArticleItem();
            article.Categories = categories;
            article.CategoryId = categories.First().Value;
            return Task.FromResult(article);
        }

        public Task<ArticleItem> Get(int id)
        {
            var article = articles.SingleOrDefault(x => x.Id == id);
            article.Categories = categories;
            return Task.FromResult(article);
        }

        public Task Create(ArticleItem item)
        {
            item.Id = articles.Count() > 0 ? articles.Max(x => x.Id) + 1 : 1;
            articles.Add(item);
            return Task.CompletedTask;
        }

        public Task Update(ArticleItem item)
        {
            var article = articles.SingleOrDefault(x => x.Id == item.Id);
            if(article == null) throw new ArgumentException("article not found!");
            article.Title = item.Title;
            article.CategoryId = item.CategoryId;
            article.Content = item.Content;
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var article = articles.SingleOrDefault(x => x.Id == id);
            if(article == null) throw new ArgumentException("article not found!");
            articles.Remove(article);
            return Task.CompletedTask;
        }
    }
}