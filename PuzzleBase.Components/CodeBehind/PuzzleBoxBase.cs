using Microsoft.AspNetCore.Components;
using PuzzleBase.Components.Bitmasks;
using PuzzleBase.Models.State;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleBase.Components.CodeBehind
{
    public class PuzzleBoxBase : ComponentBase, IDisposable
    {
        [Parameter]
        public int Row { get; set; }

        [Parameter]
        public int Column { get; set; }

        protected BoundaryBitmask Boundary { get; set; }

        protected int? HelperValue { get; set; }

        protected int ID => Row * 10 + Column;

        protected int? Value => State.Boxes[Row - 1, Column - 1].Value;

        protected bool IsConflicted => State.Boxes[Row - 1, Column - 1].IsConflicted;

        protected bool IsBoundaryConflicted => State.Boxes[Row - 1, Column - 1].IsBoundaryConflicted;

        protected bool IsGiven => State.Boxes[Row - 1, Column - 1].IsGiven;

        [Inject]
        public SudokuState State { get; private set; }

        protected override void OnInitialized()
        {
            Boundary = BoundaryBitmask.None;
            if (Row % 3 == 1)
                Boundary |= BoundaryBitmask.North;
            else if (Row % 3 == 0)
                Boundary |= BoundaryBitmask.South;
            if (Column % 3 == 1)
                Boundary |= BoundaryBitmask.West;
            else if (Column % 3 == 0)
                Boundary |= BoundaryBitmask.East;

            State.Boxes[Row - 1, Column - 1].OnStateChanged += base.StateHasChanged;

            base.OnInitialized();
        }

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
                State.Boxes[Row - 1, Column - 1].Value = null;
                Helper_MouseOver(digit);
                return;
            }

            HelperValue = null;
            State.Boxes[Row - 1, Column - 1].Value = digit;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposeManagedResources)
        {
            if (disposeManagedResources)
                State.Boxes[Row - 1, Column - 1].OnStateChanged -= base.StateHasChanged;
        }
    }
}
