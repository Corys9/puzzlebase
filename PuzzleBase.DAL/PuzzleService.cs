using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PuzzleBase.DAL.Models;

using Context = PuzzleBase.DAL.DB.PuzzlebaseContext;

namespace PuzzleBase.DAL
{
    public class PuzzleService : IPuzzleService
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
                              Difficulty = p.Difficulty
                          };
            return puzzles.ToList();
        }
    }
}
