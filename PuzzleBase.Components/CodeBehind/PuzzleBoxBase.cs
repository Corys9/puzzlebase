using Microsoft.AspNetCore.Components;
using PuzzleBase.Components.Bitmasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleBase.Components.CodeBehind
{
    public class PuzzleBoxBase : ComponentBase
    {
        [Parameter]
        public int Row { get; set; }

        [Parameter]
        public int Column { get; set; }

        [Parameter]
        public int? Value { get; set; }

        [Parameter]
        public bool IsGiven { get; set; }

        [Parameter]
        public BoundaryBitmask Boundary { get; set; }

        protected int ID => Row * 10 + Column;
    }
}
