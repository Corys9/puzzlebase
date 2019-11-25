using Microsoft.AspNetCore.Components;
using PuzzleBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleBase.Components.CodeBehind
{
    public class PuzzleComponentBase : ComponentBase
    {
        [Parameter]
        public PuzzleContent Content { get; set; }
    }
}
