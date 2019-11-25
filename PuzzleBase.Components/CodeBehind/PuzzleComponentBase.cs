using Microsoft.AspNetCore.Components;
using PuzzleBase.Models;
using PuzzleBase.Models.State;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleBase.Components.CodeBehind
{
    public class PuzzleComponentBase : ComponentBase
    {
        [Parameter]
        public PuzzleContent Content { get; set; }

        [Inject]
        public SudokuState State { get; set; }

        protected void Validate()
        {
            var isValid = true;

            ResetValidation();

            if (!RowsAreUnique())
                isValid = false;

            if (!ColumnsAreUnique())
                isValid = false;

            if (!RegionsAreUnique())
                isValid = false;

            if (!isValid)
                return;

            // TODO: check if it's completed
        }

        private void ResetValidation()
        {
            for (var row = 0; row < 9; ++row)
                for (var column = 0; column < 9; ++column)
                {
                    State.Boxes[row, column].IsConflicted = false;
                    State.Boxes[row, column].IsBoundaryConflicted = false;
                }
        }

        private bool RowsAreUnique()
        {
            var duplicatesFound = false;

            var seenDigits = new List<(int Row, int Column, int Value)>();
            var boxesWithDuplicates = new HashSet<(int Row, int Column)>();

            for (var row = 0; row < 9; ++row)
            {
                for (var column = 0; column < 9; ++column)
                {
                    var currentDigit = State.Boxes[row, column].Value;

                    if (!currentDigit.HasValue)
                        continue;

                    var previousCopy = seenDigits.FirstOrDefault(d => d.Value == currentDigit.Value);
                    if (previousCopy != default)
                    {
                        duplicatesFound = true;
                        boxesWithDuplicates.Add((row, column));
                        boxesWithDuplicates.Add((previousCopy.Row, previousCopy.Column));
                        continue;
                    }

                    seenDigits.Add((row, column, currentDigit.Value));
                }

                seenDigits.Clear();
            }

            foreach (var (Row, Column) in boxesWithDuplicates)
                State.Boxes[Row, Column].IsConflicted = true;

            return !duplicatesFound;
        }

        private bool ColumnsAreUnique()
        {
            var duplicatesFound = false;

            var seenDigits = new List<(int Row, int Column, int Value)>();
            var boxesWithDuplicates = new HashSet<(int Row, int Column)>();

            for (var column = 0; column < 9; ++column)
            {
                for (var row = 0; row < 9; ++row)
                {
                    var currentDigit = State.Boxes[row, column].Value;

                    if (!currentDigit.HasValue)
                        continue;

                    var previousCopy = seenDigits.FirstOrDefault(d => d.Value == currentDigit.Value);
                    if (previousCopy != default)
                    {
                        duplicatesFound = true;
                        boxesWithDuplicates.Add((row, column));
                        boxesWithDuplicates.Add((previousCopy.Row, previousCopy.Column));
                        continue;
                    }

                    seenDigits.Add((row, column, currentDigit.Value));
                }

                seenDigits.Clear();
            }

            foreach (var (row, column) in boxesWithDuplicates)
                State.Boxes[row, column].IsConflicted = true;

            return !duplicatesFound;
        }

        private bool RegionsAreUnique()
        {
            var duplicatesFound = false;

            var seenDigits = new List<(int Row, int Column, int Value)>();
            var regionsWithDuplicates = new HashSet<int>();

            for (var i = 0; i < State.Regions.Count; ++i)
            {
                foreach (var (row, column) in State.Regions[i])
                {
                    var currentDigit = State.Boxes[row, column].Value;

                    if (!currentDigit.HasValue)
                        continue;

                    var previousCopy = seenDigits.FirstOrDefault(d => d.Value == currentDigit.Value);
                    if (previousCopy != default)
                    {
                        duplicatesFound = true;
                        regionsWithDuplicates.Add(i);
                        continue;
                    }

                    seenDigits.Add((row, column, currentDigit.Value));
                }

                seenDigits.Clear();
            }

            foreach (var regionIndex in regionsWithDuplicates)
                for (var i = 0; i < 9; ++i)
                {
                    State.Boxes[
                        State.Regions[regionIndex][i].Row, 
                        State.Regions[regionIndex][i].Column
                    ].IsBoundaryConflicted = true;
                }

            return !duplicatesFound;
        }
    }
}
