using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Model
{
    [Table("Comments")]
    public class Comments
    {
        public int id { get; set; }
        public string title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int? stockId { get; set; }
        public Stock? Stock { get; set; }

        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}