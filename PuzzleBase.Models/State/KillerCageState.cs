using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleBase.Models.State
{
    public class KillerCageState : KillerCage
    {
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
