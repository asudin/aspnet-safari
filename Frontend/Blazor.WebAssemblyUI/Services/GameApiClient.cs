using Blazor.WebAssemblyUI.Models;
using System.Net.Http.Json;

namespace Blazor.WebAssemblyUI.Services
{
    public class GameApiClient
    {
        private readonly string _apiUrl = "https://localhost:7104/";
        private readonly HttpClient _httpClient;

        public GameApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task StartGameAsync()
        {
            await _httpClient.PostAsync($"{_apiUrl}Game/startGame", null);
        }

        public async Task<FrontendGameStateDto> GetGameStateAsync()
        {
            return await _httpClient.GetFromJsonAsync<FrontendGameStateDto>($"{_apiUrl}Game/gameState");
        }

        public async Task<string> GetAnimalInfoAsync()
        {
            return await _httpClient.GetStringAsync($"{_apiUrl}Game/animalInfo");
        }

        public async Task<string> GetUserCommands()
        {
            return await _httpClient.GetStringAsync($"{_apiUrl}Game/userCommands");
        }

        public async Task<FrontendAnimalsListDto> GetAnimalsList()
        {
            return await _httpClient.GetFromJsonAsync<FrontendAnimalsListDto>($"{_apiUrl}Game/animalList");
        }

        public async Task<bool> AddAnimalAsync(char key)
        {
            var response = await _httpClient.PostAsync($"{_apiUrl}Game/addAnimal/{key}", null);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
