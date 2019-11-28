using System;
using System.Collections.Generic;

namespace PuzzleBase.DAL.DB
{
    public partial class History
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PuzzleId { get; set; }
        public bool IsCompleted { get; set; }
        public TimeSpan? Duration { get; set; }
        public string State { get; set; }
        public DateTime LastUpdatedTs { get; set; }

        public virtual Puzzle Puzzle { get; set; }
        public virtual Aspnetusers User { get; set; }
    }
}
