using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleBase.Components.CodeBehind
{
    public class PuzzleDifficultyBase : ComponentBase
    {
        [Parameter]
        public int? Difficulty { get; set; }
    }
}
