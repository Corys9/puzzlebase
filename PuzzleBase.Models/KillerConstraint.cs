using System.Collections.Generic;

namespace PuzzleBase.Models
{
    public class KillerConstraint : PuzzleConstraint
    {
        public List<KillerCage> Cages { get; set; }
    }
}
