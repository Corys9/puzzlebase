using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleBase.DAL.Models
{
    public class Puzzle
    {
        public int ID { get; set; }

        public string Content { get; set; }

        public DateTime CreatedTS { get; set; }

        public int? Difficulty { get; set; }

        public bool IsActive { get; set; }

        public Author Author { get; set; }
    }
}
