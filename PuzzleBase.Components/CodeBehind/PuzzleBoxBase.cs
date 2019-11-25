using Microsoft.AspNetCore.Components;
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

        protected int ID => Row * 10 + Column;
    }
}
