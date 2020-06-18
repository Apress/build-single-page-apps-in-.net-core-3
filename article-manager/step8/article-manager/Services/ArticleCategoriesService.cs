using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using article_manager.Models;
using backend.Data;
using backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace article_manager.Services
{
    public class ArticleCategoriesService
        : ICRUDService<ArticleCategoryListItem, ArticleCategoryItem>
    {
        private readonly ApplicationDbContext db;

        public ArticleCategoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Task<ArticleCategoryListItem[]> GetList()
        {
            return this.db.ArticleCategories
                .Select(x => new ArticleCategoryListItem() 
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToArrayAsync();
        }

        public Task<ArticleCategoryItem> GetNew()
        {
            return Task.FromResult(
                new ArticleCategoryItem()
            );
        }
        public Task<ArticleCategoryItem> Get(int id)
        {
            return this.db.ArticleCategories
                .Where(x => x.Id == id)
                .Select(x => new ArticleCategoryItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).SingleOrDefaultAsync();
        }

        public async Task Create(ArticleCategoryItem item)
        {
            var entity = new ArticleCategory()
            {
                Name = item.Name,
                Description = item.Description,
            };
            this.db.Add(entity);
            await this.db.SaveChangesAsync();
            item.Id = entity.Id;
        }

        public Task Update(ArticleCategoryItem item)
        {
            var entity = this.db.ArticleCategories.SingleOrDefault(x => x.Id == item.Id);
            if(entity == null) throw new ArgumentException("item not found", "item");
            entity.Name = item.Name;
            entity.Description = item.Description;
            return this.db.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            var entity = this.db.ArticleCategories.SingleOrDefault(x => x.Id == id);
            if(entity == null) throw new ArgumentException("item not found", "item");
            this.db.Remove(entity);
            return this.db.SaveChangesAsync();
        }
    }
}