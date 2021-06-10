using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        [Required]
        public string PublisherName{ get; set; }
        [Required]
        [StringLength(500,MinimumLength =1)]
        public string  PostContent{ get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<Comment> comments { get; set; } = new HashSet<Comment>();


    }
}
