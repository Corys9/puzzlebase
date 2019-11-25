using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleBase.Models.State
{
    public class SudokuState
    {
        public BoxState[,] Boxes { get; private set; }

        public SudokuState()
        {
            Boxes = new BoxState[9, 9];
            for (var row = 0; row < 9; ++row)
                for (var column = 0; column < 9; ++column)
                    Boxes[row, column] = new BoxState();
        }
    }
}
