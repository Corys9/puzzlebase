using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PuzzleBase.Models.ViewModels
{
    public class PuzzleForm
    {
        [StringLength(45, ErrorMessage = "Author's name cannot exceed 45 characters.")]
        public string AuthorName { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "Content cannot exceed 2000 characters.")]
        public string Content { get; set; }

        [Range(0, 15, ErrorMessage = "Difficulty must be in 0-15 range.")]
        public int Difficulty { get; set; }

        public string UserID { get; set; }
    }
}
