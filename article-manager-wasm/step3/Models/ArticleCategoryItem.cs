using System.ComponentModel.DataAnnotations;

namespace article_manager_wasm.Models
{
    public class ArticleCategoryItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}