using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using PuzzleBase.Models;

using Context = PuzzleBase.DAL.DB.PuzzlebaseContext;
using System;
using PuzzleBase.Models.ViewModels;

namespace PuzzleBase.DAL
{
    public class PuzzleRepository : IPuzzleRepository
    {
        public List<Puzzle> GetPuzzleList()
        {
            using var db = new Context();

            var puzzles = from p in db.Puzzle.Include(p => p.Author)
                          where p.IsActive
                          orderby p.Id
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

        public Puzzle GetPuzzleByID(int puzzleID)
        {
            using var db = new Context();

            var puzzle = from p in db.Puzzle.Include(p => p.Author)
                         where p.Id == puzzleID
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
                             Content = JsonConvert.DeserializeObject<PuzzleContent>(p.Content),
                             CreatedTS = p.CreatedTs,
                             Difficulty = p.Difficulty,
                             IsActive = p.IsActive
                         };

            return puzzle.FirstOrDefault();
        }

        public int AddPuzzle(PuzzleForm puzzle)
        {
            using var db = new Context();

            var existingPuzzle = (from p in db.Puzzle
                                  where p.Content == puzzle.Content
                                  select p).FirstOrDefault();

            if (existingPuzzle != null)
                return 2; // duplicate

            DB.Author existingAuthor = null;

            if (!string.IsNullOrWhiteSpace(puzzle.AuthorName))
            {
                existingAuthor = (from a in db.Author
                                  where a.Name == puzzle.AuthorName
                                  select a).FirstOrDefault();

                if (existingAuthor == null) // new author
                {
                    try
                    {
                        var author = new DB.Author { Name = puzzle.AuthorName };
                        db.Author.Add(author);
                        db.SaveChanges();

                        existingAuthor.Id = author.Id;
                    }
                    catch (Exception ex)
                    {
                        return 1; // error
                    }
                }
            }

            try
            {
                var puzzleEntity = new DB.Puzzle
                {
                    AuthorId = existingAuthor?.Id,
                    Content = puzzle.Content,
                    CreatedTs = DateTime.UtcNow,
                    Difficulty = puzzle.Difficulty == 0 ? null : (int?)puzzle.Difficulty,
                    IsActive = true,
                    OwnerId = puzzle.UserID
                };

                db.Puzzle.Add(puzzleEntity);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return 1; // error
            }

            return 0;
        }
    }
}
