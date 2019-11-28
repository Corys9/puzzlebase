using System;
using System.Collections.Generic;

namespace PuzzleBase.DAL.DB
{
    public partial class Efmigrationshistory
    {
        public string MigrationId { get; set; }
        public string ProductVersion { get; set; }
    }
}
