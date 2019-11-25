using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleBase.Models.State
{
    public class BoxState
    {
        private int? _value;
        private bool _isConflicted;
        private bool _isGiven;

        public int? Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged?.Invoke();
            }
        }

        public bool IsConflicted
        {
            get => _isConflicted;
            set
            {
                _isConflicted = value;
                StateChanged();
            }
        }

        public bool IsGiven
        {
            get => _isGiven;
            set
            {
                _isGiven = value;
                StateChanged();
            }
        }

        public event Action OnStateChanged;

        public event Action OnValueChanged;

        private void StateChanged() => OnStateChanged?.Invoke();
    }
}
