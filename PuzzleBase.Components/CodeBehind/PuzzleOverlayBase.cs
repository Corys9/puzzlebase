using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleBase.Components.CodeBehind
{
    public class PuzzleOverlayBase : ComponentBase
    {
        [Parameter]
        public string Message { get; set; }
    }
}
