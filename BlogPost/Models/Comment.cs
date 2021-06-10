using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        public string CommentPublisherName { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 1)]
        public string CommentContent { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int PostId { get; set; }
        public virtual Post post { get; set; }
    }
}
