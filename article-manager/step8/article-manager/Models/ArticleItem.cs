using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using frontendlib.Models;

namespace article_manager.Models
{
    public class ArticleItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Title is too long.")]
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public InputSelectItem[] Categories { get; set; }
        public string Content { get; set; }
    }
}