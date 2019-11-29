using Microsoft.AspNetCore.Components;
using PuzzleBase.Models.State;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleBase.Components.CodeBehind
{
    public class PuzzleControlsBase : ComponentBase
    {
        [Inject]
        public SudokuState State { get; private set; }

        protected void SetInputMethod(InputMode inputMode) => State.InputMode = inputMode;
    }
}
