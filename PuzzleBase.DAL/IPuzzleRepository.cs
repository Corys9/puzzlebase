using PuzzleBase.Models;
using System.Collections.Generic;

namespace PuzzleBase.DAL
{
    public interface IPuzzleRepository
    {
        List<Puzzle> GetPuzzleList();
    }
}
