using System;
using System.Collections.Generic;

namespace PuzzleBase.DAL.DB
{
    public partial class User
    {
        public User()
        {
            History = new HashSet<History>();
            Puzzle = new HashSet<Puzzle>();
        }

        public DateTime? ActivatedTs { get; set; }
        public DateTime CreatedTs { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsSuspended { get; set; }
        public DateTime? LastLoginTs { get; set; }
        public string PasswordHash { get; set; }
        public string SuspensionReason { get; set; }

        public virtual ICollection<History> History { get; set; }
        public virtual ICollection<Puzzle> Puzzle { get; set; }
    }
}
