using System.Collections.Generic;

namespace PuzzleBase.Models
{
    public class JigsawConstraint : PuzzleConstraint
    {
        public List<List<int>> Regions { get; set; }
    }
}
