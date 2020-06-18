using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using article_manager.Models;

namespace article_manager.Services
{
    public class ArticleCategoriesService
        : ICRUDService<ArticleCategoryListItem, ArticleCategoryItem>
    {
        static List<ArticleCategoryItem> categories = new List<ArticleCategoryItem>{
            new ArticleCategoryItem() { Id = 1, Name = "Category 1", Description = "Description 1" },
            new ArticleCategoryItem() { Id = 2, Name = "Category 2", Description = "Description 2" },
            new ArticleCategoryItem() { Id = 3, Name = "Category 3", Description = "Description 3" },
        };

        public Task<ArticleCategoryListItem[]> GetList()
        {
            return Task.FromResult(
                categories.Select(x => new ArticleCategoryListItem()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToArray()
            );
        }

        public Task<ArticleCategoryItem> GetNew()
        {
            return Task.FromResult(
                new ArticleCategoryItem()
            );
        }
        public Task<ArticleCategoryItem> Get(int id)
        {
            return Task.FromResult(
                categories.SingleOrDefault(x => x.Id == id)
            );
        }

        public Task Create(ArticleCategoryItem item)
        {
            item.Id = categories.Count() > 0  ? categories.Max(x => x.Id) + 1 : 1;
            categories.Add(item);
            return Task.CompletedTask;
        }

        public Task Update(ArticleCategoryItem item)
        {
            var category = categories.SingleOrDefault(x => x.Id == item.Id);
            if(category == null) throw new ArgumentException("Category not found!");
            category.Name = item.Name;
            category.Description = item.Description;
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var category = categories.SingleOrDefault(x => x.Id == id);
            if(category == null) throw new ArgumentException("Category not found!");
            categories.Remove(category);
            return Task.CompletedTask;
        }
    }
}