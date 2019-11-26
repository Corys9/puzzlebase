using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using PuzzleBase.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PuzzleBase.Web.CodeBehind
{
    public class IndexBase : ComponentBase
    {
        public List<Puzzle> Puzzles { get; private set; }

        [Inject]
        public IHttpClientFactory ClientFactory { get; private set; }

        public async Task FetchPuzzles()
        {
            var http = ClientFactory.CreateClient("puzzleAPI");
            var puzzlesJson = await http.GetStringAsync("api/puzzle");
            Puzzles = JsonConvert.DeserializeObject<List<Puzzle>>(puzzlesJson);
        }

        protected override async Task OnInitializedAsync() =>
            await FetchPuzzles();
    }
}
