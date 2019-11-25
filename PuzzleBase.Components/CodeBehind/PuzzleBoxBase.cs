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

        protected int? HelperValue { get; set; }

        protected int ID => Row * 10 + Column;

        protected void Helper_MouseOver(int digit)
        {
            if (!Value.HasValue)
                HelperValue = digit;
        }

        protected void Helper_MouseOut()
        {
            HelperValue = null;
        }

        protected void Helper_Click(int digit)
        {
            if (IsGiven)
                return;

            if (Value.HasValue)
            {
                Value = null;
                Helper_MouseOver(digit);
                return;
            }

            Value = digit;
            HelperValue = null;
        }
    }
}
