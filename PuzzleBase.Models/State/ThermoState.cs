using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleBase.Models.State
{
    public class ThermoState
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public string Type { get; set; }

        public bool IsBulb { get; set; }

        private bool _isConflicted;

        public bool IsConflicted
        {
            get => _isConflicted;
            set
            {
                _isConflicted = value;
                OnStateChanged?.Invoke();
            }
        }

        public event Action OnStateChanged;
    }
}
