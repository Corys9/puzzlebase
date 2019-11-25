using Microsoft.AspNetCore.Components;
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
            Puzzles = await http.GetJsonAsync<List<Puzzle>>("api/puzzle");
        }

        protected override async Task OnInitializedAsync() =>
            await FetchPuzzles();
    }
}
