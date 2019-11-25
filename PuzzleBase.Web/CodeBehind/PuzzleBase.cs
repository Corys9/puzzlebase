using Microsoft.AspNetCore.Components;
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
            Puzzle = await http.GetJsonAsync<Puzzle>($"api/puzzle/{ID}");
        }

        protected override async Task OnInitializedAsync() =>
            await FetchPuzzle();
    }
}
