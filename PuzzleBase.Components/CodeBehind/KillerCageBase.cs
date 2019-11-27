using Microsoft.AspNetCore.Components;
using PuzzleBase.Components.Bitmasks;

namespace PuzzleBase.Components.CodeBehind
{
    public class KillerCageBase : ComponentBase
    {
        [Parameter]
        public CageBitmask Elements { get; set; }

        [Parameter]
        public bool ShowSum { get; set; }

        [Parameter]
        public int? Sum { get; set; }

        [Parameter]
        public bool IsConflicted { get; set; }
    }
}
