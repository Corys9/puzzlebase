using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using PuzzleBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PuzzleBase.Web.CodeBehind
{
    public class PuzzleBase : ComponentBase
    {
        [Parameter]
        public int ID { get; set; }

        public Puzzle Puzzle { get; set; }

        [Inject]
        public IHttpClientFactory ClientFactory { get; private set; }

        public async Task FetchPuzzle()
        {
            var http = ClientFactory.CreateClient("puzzleAPI");
            var puzzleJson = await http.GetStringAsync($"api/puzzle/{ID}");
            Puzzle = JsonConvert.DeserializeObject<Puzzle>(puzzleJson);
        }

        protected override async Task OnInitializedAsync() =>
            await FetchPuzzle();
    }
}
