using System;
using System.Collections.Generic;

namespace PuzzleBase.DAL.DB
{
    public partial class Puzzle
    {
        public Puzzle()
        {
            History = new HashSet<History>();
        }

        public int? AuthorId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTs { get; set; }
        public int? Difficulty { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int? OwnerId { get; set; }

        public virtual Author Author { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<History> History { get; set; }
    }
}
