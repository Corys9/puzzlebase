using Microsoft.AspNetCore.Components;
using PuzzleBase.Models.State;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleBase.Components.CodeBehind
{
    public class ThermosBase : ComponentBase
    {
        [Inject]
        public SudokuState State { get; private set; }
    }
}
