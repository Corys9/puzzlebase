using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PuzzleBase.Models;

using Context = PuzzleBase.DAL.DB.PuzzlebaseContext;

namespace PuzzleBase.DAL
{
    public class PuzzleRepository : IPuzzleRepository
    {
        public List<Puzzle> GetPuzzleList()
        {
            using var db = new Context();

            var puzzles = from p in db.Puzzle.Include(p => p.Author)
                          where p.IsActive
                          select new Puzzle
                          {
                              ID = p.Id,
                              Author = p.Author != null
                                ? new Author
                                {
                                    ID = p.Author.Id,
                                    Name = p.Author.Name,
                                    WebSite = p.Author.WebSite
                                }
                                : null,
                              Content = null,
                              CreatedTS = p.CreatedTs,
                              Difficulty = p.Difficulty,
                              IsActive = p.IsActive
                          };

            return puzzles.ToList();
        }
    }
}
