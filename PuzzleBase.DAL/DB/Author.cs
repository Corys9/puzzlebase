using System;
using System.Collections.Generic;

namespace PuzzleBase.DAL.DB
{
    public partial class Author
    {
        public Author()
        {
            Puzzle = new HashSet<Puzzle>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string WebSite { get; set; }

        public virtual ICollection<Puzzle> Puzzle { get; set; }
    }
}
