using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleBase.Models.State
{
    public class SudokuState
    {
        public BoxState[,] Boxes { get; set; }

        public List<List<(int Row, int Column)>> Regions { get; set; }

        public List<List<ThermoState>> Thermos { get; set; }

        public (int Row, int Column)? ActiveBox { get; set; }
    }
}
