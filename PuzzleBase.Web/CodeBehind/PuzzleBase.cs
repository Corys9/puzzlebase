using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using PuzzleBase.Models;
using PuzzleBase.Models.State;
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
                        IsGiven = value.HasValue
                    };
                    State.Boxes[row, column].OnValueChanged += Validate;
                }

            State.Regions = new List<List<(int Row, int Column)>>();
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

                    for (int i = 0; i < thermo.Count; i++)
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
    }
}
