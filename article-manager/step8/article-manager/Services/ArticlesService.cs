using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using article_manager.Models;
using backend.Data;
using backend.Data.Entities;
using frontendlib.Models;
using Microsoft.EntityFrameworkCore;

namespace article_manager.Services
{
    public class ArticlesService
        : ICRUDService<ArticleListItem, ArticleItem>
    {
        private readonly ApplicationDbContext db;

        public ArticlesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Task<ArticleListItem[]> GetList()
        {
            return this.db.Articles.Select(x => new ArticleListItem() 
            {
                Id = x.Id,
                Title = x.Title,
                Category = x.Category.Name    
            }).ToArrayAsync();
        }

        public async Task<ArticleItem> GetNew()
        {
            var article = new ArticleItem() 
            {
                Categories = await this.getCategories()
            };
            if(article.Categories.Count() > 0) 
            {
                article.CategoryId = article.Categories.First().Value;
            }
            return article;
        }

        public async Task<ArticleItem> Get(int id)
        {
             var article = await this.db.Articles
                .Where(x => x.Id == id)
                .Select(x => new ArticleItem()
                {
                    Id = x.Id,
                    Title = x.Title,
                    CategoryId = x.CategoryId,
                    Content = x.Content
                }).SingleOrDefaultAsync();
            
            if(article == null) throw new ArgumentException("article not found", "id");
            article.Categories = await this.getCategories();
            return article;
        }

        public async Task Create(ArticleItem item)
        {
            var article = new Article()
            {
                Title = item.Title,
                Content = item.Content,
                CategoryId = item.CategoryId
            };
            this.db.Add(article);
            await this.db.SaveChangesAsync();
            item.Id = article.Id;
        }

        public Task Update(ArticleItem item)
        {
            var article = this.db.Articles.SingleOrDefault(x => x.Id == item.Id);
            if(article == null) throw new ArgumentException("article not found", "item");
            article.Title = item.Title;
            article.Content = item.Content;
            article.CategoryId = item.CategoryId;
            return this.db.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            var article = this.db.Articles.SingleOrDefault(x => x.Id == id);
            if(article == null) throw new ArgumentException("article not found", "id");
            this.db.Remove(article);
            return this.db.SaveChangesAsync();
        }

        private Task<InputSelectItem[]> getCategories() 
        {
            return this.db.ArticleCategories
                .Select(x => new InputSelectItem()
                {
                    Value = x.Id,
                    Label = x.Name
                }).ToArrayAsync();
        }
    }
}