using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleBase.Components.Bitmasks
{
    [Flags]
    public enum BoundaryBitmask
    {
        None = 0,
        West = 1,
        South = 2,
        East = 4,
        North = 8
    }
}
