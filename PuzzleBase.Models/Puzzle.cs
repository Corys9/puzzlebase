using System;
using System.Text;

namespace PuzzleBase.Models
{
    public class Puzzle
    {
        public int ID { get; set; }

        public PuzzleContent Content { get; set; }

        public DateTime CreatedTS { get; set; }

        public int? Difficulty { get; set; }

        public bool IsActive { get; set; }

        public Author Author { get; set; }
    }
}
