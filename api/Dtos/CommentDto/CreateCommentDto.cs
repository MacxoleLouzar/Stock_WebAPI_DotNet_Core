using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.CommentDto
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Min characters must be 3!")]
        [MaxLength(100, ErrorMessage = "Max characters must be 100!")]

        public string title { get; set; } = string.Empty;
        [Required]
        [MinLength(3, ErrorMessage = "Min characters must be 3!")]
        [MaxLength(1000, ErrorMessage = "Max characters must be 1000!")]
        public string Content { get; set; } = string.Empty;

    }
}