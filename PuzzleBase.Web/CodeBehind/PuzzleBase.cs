using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using PuzzleBase.Models;
using PuzzleBase.Models.State;
using PuzzleBase.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PuzzleBase.Web.CodeBehind
{
    public class PuzzleBase : ComponentBase
    {
        [Parameter]
        public int ID { get; set; }

        protected Puzzle Puzzle { get; set; }

        protected bool IsCompleted { get; set; }

        [Inject]
        public IHttpClientFactory ClientFactory { get; private set; }

        [Inject]
        public SudokuState State { get; set; }

        protected override async Task OnInitializedAsync() =>
            await FetchPuzzle();

        public async Task FetchPuzzle()
        {
            var http = ClientFactory.CreateClient("puzzleAPI");
            var puzzleJson = await http.GetStringAsync($"api/puzzle/{ID}");
            Puzzle = JsonConvert.DeserializeObject<Puzzle>(puzzleJson);

            InitState();
        }

        private void InitState()
        {
            State.Boxes = new BoxState[9, 9];
            for (var row = 0; row < 9; ++row)
                for (var column = 0; column < 9; ++column)
                {
                    var value = Puzzle.Content.Givens?
                        .FirstOrDefault(g => g[0] == row + 1 && g[1] == column + 1)?[2];

                    State.Boxes[row, column] = new BoxState
                    {
                        Value = value,
                        IsGiven = value.HasValue,
                        CornerHelpers = Enumerable.Repeat(false, 9).ToList(),
                        CentralValue = string.Empty
                    };
                    State.Boxes[row, column].OnValueChanged += Validate;
                }

            State.Regions = new List<List<(int Row, int Column)>>();
            if (Puzzle.Content.Constraints != null &&
                Puzzle.Content.Constraints
                    .FirstOrDefault(c => c.GetType() == typeof(JigsawConstraint))
                    is JigsawConstraint jigsawConstraint &&
                jigsawConstraint.Regions != null)
            {
                for (var region = 0; region < jigsawConstraint.Regions.Count; region++)
                {
                    State.Regions.Add(new List<(int Row, int Column)>());

                    foreach (var box in jigsawConstraint.Regions[region])
                        State.Regions[region].Add((box / 10 - 1, box % 10 - 1));
                }
            }
            else // default regions
            {
                for (var region = 0; region < 9; ++region)
                {
                    State.Regions.Add(new List<(int Row, int Column)>());

                    for (var counter = 0; counter < 3; ++counter)
                    {
                        State.Regions[region].Add((region / 3 * 3 + counter, region % 3 * 3));
                        State.Regions[region].Add((region / 3 * 3 + counter, region % 3 * 3 + 1));
                        State.Regions[region].Add((region / 3 * 3 + counter, region % 3 * 3 + 2));
                    }
                }
            }

            State.KillerCages = null;

            if (Puzzle.Content.Constraints == null)
                return;

            if (Puzzle.Content.Constraints
                    .FirstOrDefault(c => c.GetType() == typeof(ThermoConstraint))
                    is ThermoConstraint thermoConstraint &&
                thermoConstraint.Thermos != null)
            {
                State.Thermos = new List<List<ThermoState>>();

                foreach (var thermo in thermoConstraint.Thermos)
                {
                    var thermoStates = new List<ThermoState>();

                    for (var i = 0; i < thermo.Count; ++i)
                    {
                        var thermoElement = thermo[i];
                        var thermoState = new ThermoState
                        {
                            Row = thermoElement.Row,
                            Column = thermoElement.Column,
                            Type = thermoElement.Type,
                            IsBulb = i == 0
                        };
                        thermoStates.Add(thermoState);
                    }

                    State.Thermos.Add(thermoStates);
                }
            }

            if (Puzzle.Content.Constraints
                    .FirstOrDefault(c => c.GetType() == typeof(KillerConstraint))
                    is KillerConstraint killerConstraint &&
                killerConstraint.Cages != null)
            {
                State.KillerCages = new List<KillerCageState>();

                foreach (var cage in killerConstraint.Cages)
                {
                    var cageState = new KillerCageState
                    {
                        Sum = cage.Sum,
                        Boxes = cage.Boxes.Copy()
                    };
                    State.KillerCages.Add(cageState);
                }
            }
        }

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

            if (Puzzle.Content.Constraints != null && !ConstraintsAreMet())
                isValid = false;

            if (!isValid)
                return;

            IsCompleted = PuzzleIsFull();
            if (IsCompleted)
                StateHasChanged();
        }

        private void ResetValidation()
        {
            for (var row = 0; row < 9; ++row)
                for (var column = 0; column < 9; ++column)
                {
                    State.Boxes[row, column].IsConflicted = false;
                    State.Boxes[row, column].IsBoundaryConflicted = false;
                }

            if (State.KillerCages != null)
                foreach (var cage in State.KillerCages)
                    cage.IsConflicted = false;
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
            var boxesWithDuplicates = new HashSet<(int Row, int Column)>();

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
                        boxesWithDuplicates.Add((row, column));
                        boxesWithDuplicates.Add((previousCopy.Row, previousCopy.Column));
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

            foreach (var (row, column) in boxesWithDuplicates)
                State.Boxes[row, column].IsConflicted = true;

            return !duplicatesFound;
        }

        private bool PuzzleIsFull()
        {
            for (var row = 0; row < 9; ++row)
                for (var column = 0; column < 9; ++column)
                {
                    if (!State.Boxes[row, column].Value.HasValue)
                        return false;
                }

            return true;
        }

        private bool ConstraintsAreMet()
        {
            foreach (var constraint in Puzzle.Content.Constraints)
            {
                if (constraint is KillerConstraint && !KillerConstraintIsMet())
                    return false;
            }

            return true;
        }

        /// <summary>
        /// All killer cages must contain unique digits within them, and the sum of placed digits cannot exceed the cage sum, if the sum is specified. If the cage is filled, the sum of all digits must equal the cage sum, if the sum is specified.
        /// </summary>
        private bool KillerConstraintIsMet()
        {
            var killerConstraintIsMet = true;

            var seenDigits = new List<(int Row, int Column, int Value)>();
            var conflictedCages = new HashSet<int>();
            var boxesWithDuplicates = new HashSet<(int Row, int Column)>();

            for (var i = 0; i < State.KillerCages.Count; ++i)
            {
                var cage = State.KillerCages[i];
                var sum = 0;
                var numberOfDigitsInTheCage = 0;

                foreach (var box in cage.Boxes)
                {
                    var row = box / 10 - 1;
                    var column = box % 10 - 1;
                    var value = State.Boxes[row, column].Value;
                    sum += value ?? 0;

                    if (!value.HasValue)
                        continue;

                    ++numberOfDigitsInTheCage;

                    var previousCopy = seenDigits.FirstOrDefault(d => d.Value == value.Value);
                    if (previousCopy != default)
                    {
                        killerConstraintIsMet = false;
                        conflictedCages.Add(i);
                        boxesWithDuplicates.Add((row, column));
                        boxesWithDuplicates.Add((previousCopy.Row, previousCopy.Column));
                        continue;
                    }

                    seenDigits.Add((row, column, value.Value));
                }

                seenDigits = new List<(int Row, int Column, int Value)>();

                var cageIsFilled = numberOfDigitsInTheCage == cage.Boxes.Count;

                if (cage.Sum.HasValue &&
                    (sum > cage.Sum.Value || (sum < cage.Sum.Value && cageIsFilled)))
                {
                    killerConstraintIsMet = false;
                    conflictedCages.Add(i);
                }
            }

            foreach (var cageIndex in conflictedCages)
            {
                var cage = State.KillerCages[cageIndex];
                cage.IsConflicted = true;
            }

            foreach (var (row, column) in boxesWithDuplicates)
                State.Boxes[row, column].IsConflicted = true;

            return killerConstraintIsMet;
        }
    }
}
