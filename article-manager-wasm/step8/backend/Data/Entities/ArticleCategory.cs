using System.ComponentModel.DataAnnotations;

namespace backend.Data.Entities
{
    public class ArticleCategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}