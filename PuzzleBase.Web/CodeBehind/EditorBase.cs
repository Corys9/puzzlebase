using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using PuzzleBase.Models;
using PuzzleBase.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PuzzleBase.Web.CodeBehind
{
    public class EditorBase : ComponentBase
    {
        protected PuzzleForm FormModel { get; set; }

        protected string ErrorMessage { get; set; }

        [Inject]
        public IHttpClientFactory ClientFactory { get; private set; }

        [Inject]
        public NavigationManager NavigationManager { get; private set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; private set; }

        [Inject]
        public UserManager<IdentityUser> UserManager { get; set; }

        protected override void OnInitialized()
        {
            FormModel = new PuzzleForm();

            base.OnInitialized();
        }

        protected async void HandleValidSubmit()
        {
            #region Fetch UserID
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (!user.Identity.IsAuthenticated)
            {
                ErrorMessage = "Please sign in before submitting a puzzle.";
                StateHasChanged();
                return;
            }

            var currentUser = await UserManager.GetUserAsync(user);
            FormModel.UserID = currentUser.Id;
            #endregion

            #region Validate the puzzle
            PuzzleContent content;
            try
            {
                content = JsonConvert.DeserializeObject<PuzzleContent>(FormModel.Content);
            }
            catch
            {
                ErrorMessage = "Puzzle content is not a valid JSON.";
                StateHasChanged();
                return;
            }

            var numberOfSolutions = GetNumberOfSolutions(content);
            if (numberOfSolutions != 1)
            {
                ErrorMessage = $"Puzzle has {(numberOfSolutions == 0 ? "no" : numberOfSolutions.ToString())} solutions!";
                StateHasChanged();
                return;
            }

            FormModel.Content = JsonConvert.SerializeObject(content);
            #endregion

            var http = ClientFactory.CreateClient("puzzleAPI");
            var statusCode = await http.PostJsonAsync<int>("api/puzzle", FormModel);

            if (statusCode == 0) // successful
            {
                NavigationManager.NavigateTo("/");
                return;
            }

            ErrorMessage = statusCode switch
            {
                2 => "Duplicate puzzle.",
                _ => "Failed to save the puzzle. Please try again later.",
            };
            StateHasChanged();
        }

        private int GetNumberOfSolutions(PuzzleContent puzzle)
        {
            // TODO: run backtrack and validate the puzzle
            return 1;
        }
    }
}
