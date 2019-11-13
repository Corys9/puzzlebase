using System;
using System.Collections.Generic;

namespace PuzzleBase.DAL.DB
{
    public partial class History
    {
        public TimeSpan? Duration { get; set; }
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime LastUpdatedTs { get; set; }
        public int PuzzleId { get; set; }
        public string State { get; set; }
        public int UserId { get; set; }

        public virtual Puzzle Puzzle { get; set; }
        public virtual User User { get; set; }
    }
}
