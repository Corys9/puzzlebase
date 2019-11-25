using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PuzzleBase.DAL;
using PuzzleBase.Models;

namespace PuzzleBase.API.Controllers
{
    [ApiController]
    [Route("api/puzzle")]
    public class PuzzleController : ControllerBase
    {
        private readonly IPuzzleRepository PuzzleRepository;

        public PuzzleController(IPuzzleRepository puzzleRepository)
        {
            PuzzleRepository = puzzleRepository;
        }

        /// <summary>
        /// Fetch the list of puzzles without content.
        /// </summary>
        /// <returns>List of puzzles</returns>
        [HttpGet]
        public List<Puzzle> GetPuzzles()
        {
            return PuzzleRepository.GetPuzzleList();
        }

        /// <summary>
        /// Fetch a puzzle by ID.
        /// </summary>
        /// <param name="puzzleID">Puzzle ID</param>
        /// <returns>Puzzle object</returns>
        [HttpGet, Route("{puzzleID:int}")]
        public Puzzle GetPuzzleByID(int puzzleID)
        {
            return PuzzleRepository.GetPuzzleByID(puzzleID);
        }
    }
}
