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

        [HttpGet]
        public List<Puzzle> GetPuzzles()
        {
            return PuzzleRepository.GetPuzzleList();
        }
    }
}
