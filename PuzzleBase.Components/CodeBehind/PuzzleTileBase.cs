using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using PuzzleBase.Models;

namespace PuzzleBase.Components.CodeBehind
{
    public class PuzzleTileBase : ComponentBase
    {
        [Parameter]
        public Puzzle Puzzle { get; set; }
    }
}
