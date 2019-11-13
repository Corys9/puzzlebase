using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleBase.DAL.Models
{
    public class User
    {
        public int ID { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; } // activated + not suspended
    }
}
