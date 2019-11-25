using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleBase.Models.State
{
    public class SudokuState
    {
        public BoxState[,] Boxes { get; private set; }

        public List<List<(int Row, int Column)>> Regions { get; private set; }

        public SudokuState()
        {
            Boxes = new BoxState[9, 9];
            for (var row = 0; row < 9; ++row)
                for (var column = 0; column < 9; ++column)
                    Boxes[row, column] = new BoxState();

            Regions = new List<List<(int Row, int Column)>>();
            for (var region = 0; region < 9; ++region)
            {
                Regions.Add(new List<(int Row, int Column)>());

                for (var counter = 0; counter < 3; ++counter)
                {
                    Regions[region].Add((region / 3 * 3 + counter, region % 3 * 3));
                    Regions[region].Add((region / 3 * 3 + counter, region % 3 * 3 + 1));
                    Regions[region].Add((region / 3 * 3 + counter, region % 3 * 3 + 2));
                }
            }
        }
    }
}
