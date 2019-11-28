using System;
using System.Collections.Generic;

namespace PuzzleBase.DAL.DB
{
    public partial class User
    {
        public int Id { get; set; }
        public DateTime? ActivatedTs { get; set; }
        public DateTime CreatedTs { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsSuspended { get; set; }
        public DateTime? LastLoginTs { get; set; }
        public string PasswordHash { get; set; }
        public string SuspensionReason { get; set; }
    }
}
