using PuzzleBase.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PuzzleBase.DAL
{
    public interface IPuzzleService
    {
        List<Puzzle> GetPuzzleList();
    }
}
