using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleBase.Components.Bitmasks
{
    [Flags]
    public enum CageBitmask
    {
        None = 0,
        VerticalTopLeft = 1,
        VerticalTopRight = 2,
        HorizontalTopLeft = 4,
        HorizontalTop = 8,
        HorizontalTopRight = 16,
        VerticalLeft = 32,
        VerticalRight = 64,
        HorizontalBottomLeft = 128,
        HorizontalBottom = 256,
        HorizontalBottomRight = 512,
        VerticalBottomLeft = 1024,
        VerticalBottomRight = 2048
    }
}
