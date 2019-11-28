using PuzzleBase.Models;
using PuzzleBase.Models.ViewModels;
using System.Collections.Generic;

namespace PuzzleBase.DAL
{
    public interface IPuzzleRepository
    {
        List<Puzzle> GetPuzzleList();

        Puzzle GetPuzzleByID(int puzzleID);

        int AddPuzzle(PuzzleForm puzzle);
    }
}
