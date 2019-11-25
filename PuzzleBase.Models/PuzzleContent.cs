using System.Collections.Generic;

namespace PuzzleBase.Models
{
    public class PuzzleContent
    {
        /// <summary>
        /// Format: [ [row, column, value] ].
        /// Note: row and column indices are 1-based.
        /// </summary>
        public List<List<int>> Givens { get; set; }

        public List<PuzzleConstraint> Constraints { get; set; }
    }
}
