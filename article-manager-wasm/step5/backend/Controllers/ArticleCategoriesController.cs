using System.Linq;
using backend.Data;
using backend.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using shared.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleCategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public ArticleCategoriesController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.db.ArticleCategories
                .Select(x => new ArticleCategoryListItem() 
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = this.db.ArticleCategories
                .Where(x => x.Id == id)
                .Select(x => new ArticleCategoryItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).SingleOrDefault();
            
            if(result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(ArticleCategoryItem item)
        {
            if(ModelState.IsValid)
            {
                var entity = new ArticleCategory()
                {
                    Name = item.Name,
                    Description = item.Description,
                };
                this.db.Add(entity);
                this.db.SaveChanges();
                item.Id = entity.Id;
                return CreatedAtAction(nameof(Get), new { id = entity.Id }, item);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ArticleCategoryItem item)
        {
            if(ModelState.IsValid)
            {
                var entity = this.db.ArticleCategories.SingleOrDefault(x => x.Id == id);
                if(entity == null) return NotFound();
                entity.Name = item.Name;
                entity.Description = item.Description;
                this.db.SaveChanges();
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = this.db.ArticleCategories.SingleOrDefault(x => x.Id == id);
            if(entity == null) return NotFound();
            this.db.Remove(entity);
            this.db.SaveChanges();
            return NoContent();
        }
    }
}