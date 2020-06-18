using System.Linq;
using backend.Data;
using backend.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using shared.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public ArticlesController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.db.Articles.Select(x => new ArticleListItem() 
            {
                Id = x.Id,
                Title = x.Title,
                Category = x.Category.Name    
            }).ToList());
        }

        [HttpGet("new")]
        public IActionResult GetNew()
        {
            var article = new ArticleItem() 
            {
                Categories = this.getCategories()
            };
            if(article.Categories.Count() > 0) 
            {
                article.CategoryId = article.Categories.First().Value;
            }
            return Ok(article);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = this.db.Articles
                .Where(x => x.Id == id)
                .Select(x => new ArticleItem()
                {
                    Id = x.Id,
                    Title = x.Title,
                    CategoryId = x.CategoryId.ToString(),
                    Content = x.Content
                }).SingleOrDefault();
            
            if(result == null) return NotFound();
            result.Categories = this.getCategories();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(ArticleItem item)
        {
            if(ModelState.IsValid)
            {
                var entity = new Article()
                {
                    Title = item.Title,
                    Content = item.Content,
                    CategoryId = int.Parse(item.CategoryId)
                };
                this.db.Add(entity);
                this.db.SaveChanges();
                item.Id = entity.Id;
                return CreatedAtAction(nameof(Get), new { id = entity.Id }, item);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ArticleItem item)
        {
            if(ModelState.IsValid)
            {
                var entity = this.db.Articles.SingleOrDefault(x => x.Id == id);
                if(entity == null) return NotFound();
                entity.Title = item.Title;
                entity.Content = item.Content;
                entity.CategoryId = int.Parse(item.CategoryId);
                this.db.SaveChanges();
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = this.db.Articles.SingleOrDefault(x => x.Id == id);
            if(entity == null) return NotFound();
            this.db.Remove(entity);
            this.db.SaveChanges();
            return NoContent();
        }

        private InputSelectItem[] getCategories() 
        {
            return this.db.ArticleCategories
                .Select(x => new InputSelectItem()
                {
                    Value = x.Id.ToString(),
                    Label = x.Name
                }).ToArray();
        }
    }
}