using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PuzzleBase.Components.Bitmasks;
using PuzzleBase.Models.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ConstructRegionBoundaries();

            base.OnInitialized();
        }

        private void ConstructRegionBoundaries()
        {
            var region = State.Regions.FirstOrDefault(r => r.Contains((Row - 1, Column - 1)));

            Boundary = BoundaryBitmask.None;
            if (Row == 1 || !region.Contains((Row - 2, Column - 1)))
                Boundary |= BoundaryBitmask.North;
            if (Row == 9 || !region.Contains((Row, Column - 1)))
                Boundary |= BoundaryBitmask.South;
            if (Column == 1 || !region.Contains((Row - 1, Column - 2)))
                Boundary |= BoundaryBitmask.West;
            if (Column == 9 || !region.Contains((Row - 1, Column)))
                Boundary |= BoundaryBitmask.East;

            State.Boxes[Row - 1, Column - 1].OnStateChanged += base.StateHasChanged;
        }

        protected void Helper_MouseOver(int digit)
        {
            if (!Value.HasValue)
                HelperValue = digit;

            State.ActiveBox = (Row, Column);
        }

        protected void Helper_MouseOut()
        {
            HelperValue = null;

            if (State.ActiveBox == (Row, Column))
                State.ActiveBox = null;
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

        public void Box_KeyPress(KeyboardEventArgs e)
        {
            if (char.IsDigit(e.Key, 0) && e.Key != "0")
                Helper_Click(int.Parse(e.Key));
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
