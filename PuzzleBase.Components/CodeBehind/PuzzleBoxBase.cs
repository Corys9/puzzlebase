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

        protected CageBitmask KillerCageBoundary { get; set; }
        protected bool ShowKillerSum { get; set; }
        protected int? KillerSum { get; set; }

        protected int? HelperValue { get; set; }

        protected int HelperColorIndex { get; set; }

        protected string CentralValue => State.Boxes[Row, Column].CentralValue;

        protected int ID => (Row + 1) * 10 + Column + 1;

        protected int? Value => State.Boxes[Row, Column].Value;

        protected bool IsConflicted => State.Boxes[Row, Column].IsConflicted;

        protected bool IsBoundaryConflicted => State.Boxes[Row, Column].IsBoundaryConflicted;

        protected bool IsKillerCageConflicted => State.KillerCages?.FirstOrDefault(c => c.Boxes.Contains(ID))?.IsConflicted ?? false;

        protected bool IsGiven => State.Boxes[Row, Column].IsGiven;

        protected List<bool> CornerHelpers => State.Boxes[Row, Column].CornerHelpers;

        protected List<bool> CornerHelperHasFocus { get; set; }

        protected int ColorIndex => State.Boxes[Row, Column].ColorIndex;

        [Inject]
        public SudokuState State { get; private set; }

        protected override void OnInitialized()
        {
            ConstructRegionBoundaries();
            ConstructKillerCageBoundaries();

            State.Boxes[Row, Column].OnStateChanged += base.StateHasChanged;
            CornerHelperHasFocus = Enumerable.Repeat(false, 9).ToList();

            var cageThisBoxBelongTo = State.KillerCages?.FirstOrDefault(c => c.Boxes.Contains(ID));
            if (cageThisBoxBelongTo != null)
                cageThisBoxBelongTo.OnStateChanged += base.StateHasChanged;

            base.OnInitialized();
        }

        private void ConstructRegionBoundaries()
        {
            var region = State.Regions.FirstOrDefault(r => r.Contains((Row, Column)));

            Boundary = BoundaryBitmask.None;

            if (Row == 0 || !region.Contains((Row - 1, Column)))
                Boundary |= BoundaryBitmask.North;
            if (Row == 8 || !region.Contains((Row + 1, Column)))
                Boundary |= BoundaryBitmask.South;
            if (Column == 0 || !region.Contains((Row, Column - 1)))
                Boundary |= BoundaryBitmask.West;
            if (Column == 8 || !region.Contains((Row, Column + 1)))
                Boundary |= BoundaryBitmask.East;
        }

        private void ConstructKillerCageBoundaries()
        {
            if (State.KillerCages == null)
                return;

            var cage = State.KillerCages.FirstOrDefault(c => c.Boxes.Contains(ID));
            if (cage == null)
                return;

            ShowKillerSum = cage.Boxes.First() == ID;
            KillerSum = cage.Sum;

            KillerCageBoundary = CageBitmask.None;

            // find whether the adjacent slots are part of this cage
            var row = Row + 1;
            var column = Column + 1;
            var t = row > 1 && cage.Boxes.Contains((row - 1) * 10 + column);
            var b = row < 9 && cage.Boxes.Contains((row + 1) * 10 + column);
            var l = column > 1 && cage.Boxes.Contains(row * 10 + column - 1);
            var r = column < 9 && cage.Boxes.Contains(row * 10 + column + 1);
            var tl = row > 1 && column > 1 && cage.Boxes.Contains((row - 1) * 10 + column - 1);
            var tr = row > 1 && column < 9 && cage.Boxes.Contains((row - 1) * 10 + column + 1);
            var bl = row < 9 && column > 1 && cage.Boxes.Contains((row + 1) * 10 + column - 1);
            var br = row < 9 && column < 9 && cage.Boxes.Contains((row + 1) * 10 + column + 1);

            // long fragments -----------------
            if (!t)
                KillerCageBoundary |= CageBitmask.HorizontalTop;

            if (!b)
                KillerCageBoundary |= CageBitmask.HorizontalBottom;

            if (!l)
                KillerCageBoundary |= CageBitmask.VerticalLeft;

            if (!r)
                KillerCageBoundary |= CageBitmask.VerticalRight;

            // top fragments -----------------
            if (l && (!t || !tl))
                KillerCageBoundary |= CageBitmask.HorizontalTopLeft;

            if (t && (!l || !tl))
                KillerCageBoundary |= CageBitmask.VerticalTopLeft;

            if (r && (!t || !tr))
                KillerCageBoundary |= CageBitmask.HorizontalTopRight;

            if (t && (!r || !tr))
                KillerCageBoundary |= CageBitmask.VerticalTopRight;

            // bottom fragments -----------------
            if (l && (!b || !bl))
                KillerCageBoundary |= CageBitmask.HorizontalBottomLeft;

            if (b && (!l || !bl))
                KillerCageBoundary |= CageBitmask.VerticalBottomLeft;

            if (r && (!b || !br))
                KillerCageBoundary |= CageBitmask.HorizontalBottomRight;

            if (b && (!r || !br))
                KillerCageBoundary |= CageBitmask.VerticalBottomRight;
        }

        protected void Helper_MouseOver(int digit)
        {
            State.ActiveBox = (Row, Column);

            if (State.InputMode == InputMode.Color)
                HelperColorIndex = digit;

            if (Value.HasValue)
                return;

            switch (State.InputMode)
            {
                case InputMode.Main:
                    HelperValue = digit;
                    break;
                case InputMode.Corner:
                case InputMode.Center:
                    CornerHelperHasFocus[digit - 1] = true;
                    break;
            }
        }

        protected void Helper_MouseOut(int digit)
        {
            HelperValue = null;
            CornerHelperHasFocus[digit - 1] = false;
            HelperColorIndex = 0;

            if (State.ActiveBox == (Row, Column))
                State.ActiveBox = null;
        }

        protected void Helper_Click(int digit)
        {
            if (State.InputMode == InputMode.Color)
            {
                if (State.Boxes[Row, Column].ColorIndex != digit)
                    State.Boxes[Row, Column].ColorIndex = digit;
                else
                    State.Boxes[Row, Column].ColorIndex = 0;
            }

            if (IsGiven)
                return;

            if (Value.HasValue && State.InputMode != InputMode.Color)
            {
                State.Boxes[Row, Column].Value = null;
                Helper_MouseOver(digit);
                return;
            }

            HelperValue = null;

            switch (State.InputMode)
            {
                case InputMode.Main:
                    State.Boxes[Row, Column].Value = digit;
                    State.Boxes[Row, Column].CornerHelpers = Enumerable.Repeat(false, 9).ToList();
                    State.Boxes[Row, Column].CentralValue = string.Empty;
                    break;
                case InputMode.Corner:
                    State.Boxes[Row, Column].CornerHelpers[digit - 1] = !State.Boxes[Row, Column].CornerHelpers[digit - 1];
                    break;
                case InputMode.Center:
                    if (State.Boxes[Row, Column].CentralValue.Contains(digit.ToString()))
                        State.Boxes[Row, Column].CentralValue = State.Boxes[Row, Column].CentralValue.Replace(digit.ToString(), string.Empty);
                    else
                    {
                        var digits = State.Boxes[Row, Column].CentralValue.Split().ToList();
                        digits.Add(digit.ToString());
                        digits.Sort();
                        State.Boxes[Row, Column].CentralValue = string.Join(separator: null, digits);
                    }
                    break;
            }
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
                State.Boxes[Row, Column].OnStateChanged -= base.StateHasChanged;
        }
    }
}
